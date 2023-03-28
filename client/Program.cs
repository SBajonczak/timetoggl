using System;
using System.Diagnostics;
using System.IO.Ports;

namespace PipelinesTest
{


  class Program
  {
    public bool Started=false;
public static Stopwatch sw;
    
    static void Main(string[] args)
    {
        sw= new Stopwatch();
      SerialPort _port = new SerialPort("COM9", 115200, Parity.None, 8, StopBits.One);

      _port.DataReceived += PortOnDataReceived;
      _port.Open();
      
      Console.WriteLine("Press Return to Exit");
      Console.ReadLine();

      _port.Close();
      _port.DataReceived -= PortOnDataReceived;
      
      Console.WriteLine("Ended");
    }

    private static void PortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      SerialPort port = sender as SerialPort;
      var line = port.ReadLine();
      if (string.Compare(line,"pressed", StringComparison.CurrentCultureIgnoreCase)>0){
        if (sw.IsRunning){
            Console.WriteLine($"Duration: { sw.Elapsed}");
            sw.Restart();    
        }else{
            Console.WriteLine("Start");
            sw.Start();
        }
      }else{
        Console.WriteLine(line);
      }

    }
  }
}