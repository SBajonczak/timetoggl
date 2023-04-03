using client;
using System.IO.Ports;

namespace PipelinesTest
{
   
    public class Start:IDisposable
    {
        Exporter exporter = new Exporter();
        AppManager manager;
        SerialPort _port;
        private Project[] projects;
        private int SelectionIndex = 0;
        /// <summary>
        /// The Recordet times
        /// </summary>
        /// <value></value>
        public List<Recording> Recordings { get; private set; }
        
        public void StartMeasure()
        {
            this.manager.sw.Start();
        }

        public void EndMeasure()
        {
            this.manager.sw.Start();
        }


        public TimeSpan? CurrentDuration
        {
            get
            {
                return manager?.ElapsedTime;
            }
        }

        public Start(List<Project> projects)
        {            
            this.projects = projects.ToArray();
            this.Recordings = new List<Recording>();
            manager = new AppManager();
            manager.NextElement += OnNext;
            manager.PreviousElement += OnPrevious;
            manager.ButtonPressed += OnButtonPressed;

            _port = new SerialPort("COM9", 115200, Parity.None, 8, StopBits.One);
            _port.DataReceived += PortOnDataReceived;
            _port.Open();

        }
        
        
        
        public Project SelectedProject { get; private set; }

        public event EventHandler<TimerTrackingStateChangedArgs> TimerStateChanged;

        public event EventHandler<ProjectSelectedArgs> ProjectSelectionChanged;

        private void OnProjectSelectionChanged(Project selectedProject)
        {
            if (ProjectSelectionChanged != null)
            {
                ProjectSelectionChanged(this, new ProjectSelectedArgs(selectedProject));
            }
        }
        private void OnTimerStateChanged(AppManager.State before, AppManager.State after)
        {
            if (TimerStateChanged!= null)
            {
                TimerStateChanged(this, new TimerTrackingStateChangedArgs(this.CurrentDuration.GetValueOrDefault(new TimeSpan()), before, after));
            }
        }

        
        


        private void OnButtonPressed(object sender, StateArgs e)
        {
            OnTimerStateChanged(e.Before, e.After);
        }

        private void OnNext(object sender, int i)
        {
            if (this.manager.CurrentState == AppManager.State.Selecting)
            {
                SelectionIndex++;
                if (SelectionIndex > this.projects.Length - 1)
                {
                    SelectionIndex = 0;
                }
                SelectedProject = projects[SelectionIndex];

                OnProjectSelectionChanged(SelectedProject);
            }
        }

        

        private void OnPrevious(object sender, int i)
        {
            if (this.manager.CurrentState == AppManager.State.Selecting)
            {
                SelectionIndex--;
                if (SelectionIndex < 0)
                {
                    SelectionIndex = this.projects.Length - 1;
                }
                SelectedProject = projects[SelectionIndex];
                OnProjectSelectionChanged(SelectedProject);
            }
        }



        private void PortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort port = sender as SerialPort;
            var line = port.ReadLine();
            if (manager != null)
            {
                manager.DoRead(line);
            }
        }

        public void Dispose()
        {
            _port.Close();
            _port.DataReceived -= PortOnDataReceived;
        }
    }
}