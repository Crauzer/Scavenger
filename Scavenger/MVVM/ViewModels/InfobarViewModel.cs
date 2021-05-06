namespace Scavenger.MVVM.ViewModels
{
    public class InfobarViewModel : PropertyNotifier
    {
        public string Message 
        {
            get => this._message;
            set
            {
                this._message = value;
                NotifyPropertyChanged();
            }
        }
        public double Progress
        {
            get => this._progress;
            set
            {
                this._progress = value;
                NotifyPropertyChanged();
            }
        }
        public bool IsProgressIndefinite
        {
            get => this._isProgressIndefinite;
            set
            {
                this._isProgressIndefinite = value;
                NotifyPropertyChanged();
            }
        }

        private string _message;
        private double _progress;
        private bool _isProgressIndefinite;

        public void Reset()
        {
            this.Message = "";
            this.Progress = 100;
            this.IsProgressIndefinite = false;
        }
    }
}
