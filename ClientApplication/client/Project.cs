using System.ComponentModel;

namespace Hardware.Driver
{
    /// <summary>
    /// Project definition.
    /// </summary>
    public class Project : INotifyPropertyChanged
    {

        public string Title { get; set; }

        public decimal HourlyRate { get; set; }

        private bool _IsSelected;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ProjectNumber { get; set; }

        public override string ToString()
        {
            return $"{Title} ({ProjectNumber})";
        }

        public Project(string[] csvLine)
        {
            Title = csvLine[0];
            if (csvLine.Length > 1)
            {
                ProjectNumber = csvLine[1];
            }
            if (csvLine.Length > 2)
            {
                HourlyRate = decimal.Parse(csvLine[2]);
            }
        }

        public string DisplayName
        {
            get { return $"{Title} (#{ProjectNumber})"; }
        }
    }
}