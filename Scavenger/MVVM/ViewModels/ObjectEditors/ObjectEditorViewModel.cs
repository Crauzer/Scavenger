using LeagueToolkit.IO.PropertyBin;
using LeagueToolkit.Meta;

namespace Scavenger.MVVM.ViewModels.ObjectEditors
{
    public class ObjectEditorViewModel : PropertyNotifier
    {
        public BinTreeObject TreeObject { get; private set; }

        private MetaEnvironment _metaEnvironment;

        public ObjectEditorViewModel(MetaEnvironment metaEnvironment, BinTreeObject treeObject)
        {
            this.TreeObject = treeObject;
            this._metaEnvironment = metaEnvironment;
        }
    }
}
