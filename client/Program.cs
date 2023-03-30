using System;
using System.Diagnostics;
using System.IO.Ports;

namespace PipelinesTest
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Project> projects = new List<Project>();
            projects.Add(new Project() { Title = "Vorwerk", Tag = "8798798" });
            projects.Add(new Project() { Title = "Gebr. Heinemann", Tag = "8798798" });
            projects.Add(new Project() { Title = "Internes", Tag = "8798798" });
            projects.Add(new Project() { Title = "Organisation", Tag = "8798798" });
            projects.Add(new Project() { Title = "Engineering", Tag = "8798798" });
            Start s = new Start(projects);
            s.Listen();
        }
    }


    public class Start
    {
        static AppManager manager;
        SerialPort _port;
        private Project[] projects;
        private int SelectionIndex = 0;
        /// <summary>
        /// The Recordet times
        /// </summary>
        /// <value></value>
        public List<Recording> Recordings { get; private set; }


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
        }
        public void Listen()
        {
            _port.Open();

            Console.WriteLine("Press Return to Exit");
            Console.ReadLine();

            _port.Close();
            _port.DataReceived -= PortOnDataReceived;

            Console.WriteLine("Ended");
        }

        private void Printout()
        {
            foreach (var item in this.Recordings)
            {
                Console.WriteLine(item);
            }
        }



        private void OnButtonPressed(object sender, EventArgs e)
        {
            Console.WriteLine($"Current State {manager.CurrentState}");
            switch (manager.CurrentState)
            {
                case AppManager.State.Running:
                    Console.Clear();
                    Console.WriteLine("Select Project");
                    break;
                case AppManager.State.Selecting:
                    if (projects[SelectionIndex] != null)
                    {
                        Console.Clear();
                        this.Recordings.Add(new Recording(projects[SelectionIndex], manager.ElapsedTime,""));
                        Printout();
                    }
                    else
                    {
                        Console.WriteLine("No Project selected");
                    }

                    break;
            }
        }

        private void OnNext(object sender, int i)
        {
            SelectionIndex++;
            if (SelectionIndex > this.projects.Length - 1)
            {
                SelectionIndex = 0;
            }
            Console.WriteLine($"{projects[SelectionIndex]}");
        }

        private void OnPrevious(object sender, int i)
        {
            SelectionIndex--;
            if (SelectionIndex < 0)
            {
                SelectionIndex = this.projects.Length - 1;
            }
            Console.WriteLine($"{projects[SelectionIndex]}");
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

    }
}