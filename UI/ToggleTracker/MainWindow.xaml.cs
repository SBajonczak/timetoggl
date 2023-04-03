using client;
using PipelinesTest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToggleTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();

        }
        public ObservableCollection<Project> projects { get; set; }
        Start s;
        Stopwatch sw;
        MainFormState state;
        Stopwatch delay = new Stopwatch();
        Exporter exp;
        private void Init()
        {
            exp = new Exporter();
            delay.Start();
            sw = new Stopwatch();
            tmr = new System.Windows.Threading.DispatcherTimer();
            tmr.Interval = new TimeSpan(0, 0, 0, 0, 800);
            tmr.Tick += Tmr_Tick;
            this.state = new MainFormState();
            this.state.CurrentMode = "Zeit gestoppt";

            this.main.DataContext = state;
            
            this.projects = new ObservableCollection<Project>();
            var projectsList = GetProjects();
            foreach (var project in projectsList)
            {
                projects.Add(project);
            }
            container.ItemsSource = projects;
            s = new Start(projectsList);
            s.TimerStateChanged += TimerStateChanged;
            s.ProjectSelectionChanged += S_ProjectSelectionChanged;
        }


        private List<Project> GetProjects()
        {
            List<Project> returnValue = new List<Project>();
            var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser("projects.csv");
            parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            parser.SetDelimiters(new string[] { ";" });

            while (!parser.EndOfData)
            {
#pragma warning disable CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.
                string[] row = parser.ReadFields();
#pragma warning restore CS8600 // Das NULL-Literal oder ein möglicher NULL-Wert wird in einen Non-Nullable-Typ konvertiert.
                returnValue.Add(new Project(row));
            }
            return returnValue;
        }

        private void Tmr_Tick(object? sender, EventArgs e)
        {
            this.state.CurrentMode = $"Zeit läuft: {sw.Elapsed.ToString("hh':'mm':'ss")}";
            tmr.Start();
        }


        System.Windows.Threading.DispatcherTimer tmr;

        private void S_ProjectSelectionChanged(object? sender, client.ProjectSelectedArgs e)
        {
            foreach (Project p in projects)
            {
                p.IsSelected = p.ProjectNumber.Equals(e.SelectedProject.ProjectNumber);
            }
        }

        private void TimerStateChanged(object? sender, client.TimerTrackingStateChangedArgs e)
        {
            //if (delay.Elapsed.TotalSeconds > 3)
            //{
            switch (e.After)
            {
                case AppManager.State.Selecting:
                    this.state.CurrentMode = "Projekt Selektion";
                    sw.Reset();
                    sw.Stop();
                    break;
                case AppManager.State.Running:
                    this.state.CurrentMode = "Zeit läuft";
                    sw.Reset();
                    sw.Start();
                    tmr.Start();
                    break;
                case AppManager.State.Stopped:
                    sw.Stop();
                    this.state.CurrentMode = $"Zeit gestoppt! Gemessene Zeit:{sw.Elapsed.ToString("hh':'mm':'ss")}";
                    tmr.Stop();
                    exp.Write(new Recording(s.SelectedProject, sw.Elapsed, ""));
                    break;
                    delay.Reset();
                    delay.Start();
            }
        }
    }
}
