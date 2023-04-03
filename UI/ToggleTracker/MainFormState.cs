using System.ComponentModel;

namespace ToggleTracker
{
    public class MainFormState: INotifyPropertyChanged
    {
        string _CurrentMode;
        public string CurrentMode { get
            {   

                return _CurrentMode; 
            }
            set
            {
                _CurrentMode = value;
                OnPropertyChanged("CurrentMode");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
