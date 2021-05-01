using LeagueToolkit.IO.PropertyBin;
using Octokit;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Scavenger.MVVM.ViewModels
{
    public class MainWindowViewModel : PropertyNotifier
    {
        public BinTreeViewModel BinTree
        {
            get => this._binTree;
            set
            {
                this._binTree = value;
                NotifyPropertyChanged();
            }
        }
        public InfobarViewModel Infobar
        {
            get => this._infobar;
            set
            {
                this._infobar = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsGloballyEnabled
        {
            get => this._isGloballyEnabled;
            set
            {
                this._isGloballyEnabled = value;
                NotifyPropertyChanged();
            }
        }

        private BinTreeViewModel _binTree;
        private bool _isGloballyEnabled = true;
        private InfobarViewModel _infobar = new InfobarViewModel();

        public MainWindowViewModel()
        {

        }

        public void Initialize()
        {
            Config.Load();

            this.Infobar.Progress = 100;
        }

        public void LoadBinTree(BinTree binTree)
        {
            this.BinTree = new BinTreeViewModel(binTree);
        }

        public async Task UpdateHashtables()
        {
            this.Infobar.Message = "Updating hashtables...";
            this.Infobar.IsProgressIndefinite = true;
            this.IsGloballyEnabled = false;

            GitHubClient githubClient = new GitHubClient(new ProductHeaderValue("Scavenger"));
            IReadOnlyList<RepositoryContent> content = await githubClient.Repository.Content.GetAllContents("CommunityDragon", "CDTB", "cdragontoolbox");
            RepositoryContent binEntriesContent = content.FirstOrDefault(x => x.Name == "hashes.binentries.txt");
            RepositoryContent binFieldContent = content.FirstOrDefault(x => x.Name == "hashes.binfields.txt");
            RepositoryContent binHashesContent = content.FirstOrDefault(x => x.Name == "hashes.binhashes.txt");
            RepositoryContent binTypesContent = content.FirstOrDefault(x => x.Name == "hashes.bintypes.txt");
            RepositoryContent wadEntriesContent = content.FirstOrDefault(x => x.Name == "hashes.game.txt");

            await SyncHashtable(Hashtables.OBJECTS_FILE, "ObjectsHashtableCheckum", binEntriesContent);
            await SyncHashtable(Hashtables.FIELDS_FILE, "FieldsHashtableCheckum", binFieldContent);
            await SyncHashtable(Hashtables.HASHES_FILE, "HashesHashtableChecksum", binHashesContent);
            await SyncHashtable(Hashtables.TYPES_FILE, "TypesHashtableChecksum", binTypesContent);
            await SyncHashtable(Hashtables.WAD_ENTRIES_FILE, "WadEntriesHashtableChecksum", wadEntriesContent);

            this.Infobar.Message = "";
            this.Infobar.IsProgressIndefinite = false;
            this.Infobar.Progress = 100;
            this.IsGloballyEnabled = true;

            async Task SyncHashtable(string filePath, string configChecksumPath, RepositoryContent repositoryContent)
            {
                if (!File.Exists(filePath) || repositoryContent.Sha != Config.Get<string>(configChecksumPath))
                {
                    using (WebClient webClient = new WebClient())
                    {
                        await webClient.DownloadFileTaskAsync(repositoryContent.DownloadUrl, filePath);
                    }

                    Config.Set(configChecksumPath, repositoryContent.Sha);
                }
            }
        }
    }
}
