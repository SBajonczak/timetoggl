using System.ComponentModel;
using System.Windows;

namespace ToggleTracker.UI
{
    public class MainFormState : INotifyPropertyChanged
    {
        string _CurrentMode;
        string _CurrentComment;
        Visibility _CurrentCommentVisible;
        public string CurrentMode
        {
            get
            {

                return _CurrentMode;
            }
            set
            {
                _CurrentMode = value;
                OnPropertyChanged("CurrentMode");
            }
        }
        public string CurrentComment
        {
            get
            {

                return _CurrentComment;
            }
            set
            {
                _CurrentComment = value;
                OnPropertyChanged("CurrentComment");
            }
        }

        public Visibility CurrentCommentVisible
        {
            get
            {

                return _CurrentCommentVisible;
            }
            set
            {
                _CurrentCommentVisible = value;
                OnPropertyChanged("CurrentCommentVisible");
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
