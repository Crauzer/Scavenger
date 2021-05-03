using CSharpImageLibrary;
using LeagueToolkit.IO.MapGeometry;
using LeagueToolkit.IO.SimpleSkinFile;
using LeagueToolkit.IO.StaticObjectFile;
using HelixToolkit.Wpf;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Media.Imaging;

namespace Scavenger.MVVM.ViewModels
{
    public class PreviewViewModel : PropertyNotifier
    {
        public PreviewType PreviewType
        {
            get => this._previewType;
            set
            {
                this._previewType = value;
                NotifyPropertyChanged();
            }
        }
        public string ContentType
        {
            get => this._contentType;
            set
            {
                this._contentType = value;
                NotifyPropertyChanged();
            }
        }
        public ViewportViewModel Viewport
        {
            get => this._viewport;
            set
            {
                this._viewport = value;
                NotifyPropertyChanged();
            }
        }
        public BitmapSource Image
        {
            get => this._image;
            set
            {
                this._image = value;
                NotifyPropertyChanged();
            }
        }

        private PreviewType _previewType;
        private string _contentType;
        private ViewportViewModel _viewport;
        private BitmapSource _image;

        public PreviewViewModel()
        {
            this._viewport = new ViewportViewModel();
        }

        public void Preview(SimpleSkin skn)
        {
            this.Viewport.LoadMesh(skn);

            this.PreviewType = PreviewType.Viewport;
        }
        public void Preview(StaticObject staticObject)
        {
            this.Viewport.LoadMesh(staticObject);

            this.PreviewType = PreviewType.Viewport;
        }
        public void Preview(ImageEngineImage ddsImage)
        {
            this.Image = ddsImage.GetWPFBitmap(512);

            this.PreviewType = PreviewType.Image;
        }
        public void Preview(MapGeometry mgeo)
        {
            this.Viewport.LoadMap(mgeo);

            this.PreviewType = PreviewType.Viewport;
        }
        public void Preview(BitmapImage bitmap)
        {
            this.Image = bitmap;

            this.PreviewType = PreviewType.Image;
            this.ContentType = "";
        }

        public void Clear()
        {
            this.Viewport.Clear();
            this._image = null;
            this.PreviewType = PreviewType.None;
            this.ContentType = string.Empty;
        }

        public void SetViewport(HelixViewport3D viewport)
        {
            this.Viewport.SetViewport(viewport);
        }
    }

    public enum PreviewType
    {
        None,
        Viewport,
        Image
    }
}
