using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.Meta;
using System.IO;

namespace Scavenger.MVVM.ViewModels.ObjectEditors
{
    public class ObjectEditorViewModel : PropertyNotifier
    {
        public string BinName => Path.GetFileNameWithoutExtension(this.BinPath);
        public string BinPath { get; private set; }
        public BinTreeObject TreeObject { get; private set; }

        private MetaEnvironment _metaEnvironment;

        public ObjectEditorViewModel(MetaEnvironment metaEnvironment, BinTreeObject treeObject, string binPath)
        {
            this.BinPath = binPath;
            this.TreeObject = treeObject;
            this._metaEnvironment = metaEnvironment;
        }
    }
}
