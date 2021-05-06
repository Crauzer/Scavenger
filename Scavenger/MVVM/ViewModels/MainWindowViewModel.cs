using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.IO.PropertyBin.Properties;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Octokit;
using Scavenger.IO.Templates;
using Scavenger.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Scavenger.MVVM.ViewModels
{
    public class MainWindowViewModel : PropertyNotifier
    {
        public bool IsBinTreeSelected => this.SelectedBinTree is not null;
        public BinTreeViewModel SelectedBinTree
        {
            get => this._selectedBinTree;
            set
            {
                this._selectedBinTree = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(this.IsBinTreeSelected));
            }
        }
        public ObservableCollection<BinTreeViewModel> BinTrees
        {
            get => this._binTrees;
            set
            {
                this._binTrees = value;
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
        public ObservableCollection<StructureTemplate> StructureTemplates
        {
            get => this._structureTemplates;
            set
            {
                this._structureTemplates = value;
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

        private BinTreeViewModel _selectedBinTree;
        private ObservableCollection<BinTreeViewModel> _binTrees = new();
        private ObservableCollection<StructureTemplate> _structureTemplates = new();
        private bool _isGloballyEnabled = true;
        private InfobarViewModel _infobar = new();

        public MainWindowViewModel()
        {

        }

        public void Initialize()
        {
            Config.Load();
            LoadStructureTemplates();

            this.Infobar.Progress = 100;
        }
        public void LoadStructureTemplates()
        {
            Directory.CreateDirectory("Templates");

            foreach (string fileName in Directory.EnumerateFiles("Templates"))
            {
                this.StructureTemplates.Add(JsonConvert.DeserializeObject<StructureTemplate>(File.ReadAllText(fileName)));
            }
        }

        public async Task LoadBinTree(string binPath, BinTree binTree)
        {
            BinTreeViewModel binTreeViewModel = await CreateBinTreeViewModel();

            this.BinTrees.Add(binTreeViewModel);
            this.SelectedBinTree = binTreeViewModel;
            
            Task<BinTreeViewModel> CreateBinTreeViewModel()
            {
                return Task.FromResult(new BinTreeViewModel(binPath, binTree, this._structureTemplates));
            }
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

            this.Infobar.Reset();
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

        public async Task ExportStructureToTemplate(BinTreeParentViewModel parentViewModel)
        {
            ExportStructureTemplateViewModel exportStructureTemplateViewModel = await DialogHelper.ShowExportStructureTemplateDialog();
            if (exportStructureTemplateViewModel is not null)
            {
                string templateName = exportStructureTemplateViewModel.TemplateName;

                try
                {
                    Directory.CreateDirectory("Templates");

                    StructureTemplate structureTemplate;
                    if(parentViewModel is BinTreeStructureViewModel structureViewModel)
                    {
                        structureTemplate = new StructureTemplate(templateName, structureViewModel);
                    }
                    else if(parentViewModel is BinTreeEmbeddedViewModel embeddedViewModel)
                    {
                        structureTemplate = new StructureTemplate(templateName, embeddedViewModel);
                    }
                    else
                    {
                        throw new InvalidOperationException($"{nameof(parentViewModel)} is not a structure/embedded");
                    }

                    File.WriteAllText($"Templates/{templateName}.template.json", JsonConvert.SerializeObject(structureTemplate, Formatting.Indented));

                    this.StructureTemplates.Add(structureTemplate);
                }
                catch (Exception exception)
                {
                    await DialogHelper.ShowMessgeDialog($"Failed to save Structure Template\n{exception}");
                }
            }
        }

        public async Task AddFieldToStructure(BinTreeParentViewModel parentViewModel)
        {
            NewBinPropertyViewModel dialogViewModel = await DialogHelper.ShowNewBinPropertyDialog(this.StructureTemplates);
            if (dialogViewModel is not null)
            {
                BinTreeProperty newProperty = dialogViewModel.BuildProperty(parentViewModel.TreeProperty.Parent);
                BinTreePropertyViewModel newPropertyViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(parentViewModel, newProperty);

                parentViewModel.Children.Add(newPropertyViewModel);
            }
        }
        public async Task AddItemToContainer(BinTreeParentViewModel containerViewModel)
        {
            BinTreeContainer container = containerViewModel.TreeProperty as BinTreeContainer;
            NewBinPropertyViewModel dialogViewModel = await DialogHelper.ShowNewBinPropertyDialog(this.StructureTemplates, new List<BinPropertyType>() { container.PropertiesType });
            if (dialogViewModel is not null)
            {
                BinTreeProperty newProperty = dialogViewModel.BuildProperty(container);
                BinTreePropertyViewModel newPropertyViewModel = BinTreeUtilities.ConstructTreePropertyViewModel(containerViewModel, newProperty);
                if (newPropertyViewModel is not null)
                {
                    newPropertyViewModel.ShowName = false;

                    containerViewModel.Children.Add(newPropertyViewModel);
                }
            }
        }
    }
}
