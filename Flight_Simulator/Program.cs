using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using airplane;
using dispatcher;

namespace Flight_Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dispatcher> dispatchers = new List<Dispatcher>() {
                new Dispatcher("London dispatcher"),
                new Dispatcher("Berlin dispatcher"),
                new Dispatcher("Rome dispatcher"),
                new Dispatcher("Warsaw dispatcher"),
                new Dispatcher("Kiev dispatcher")
            };

            Airplane airplane = new Airplane(dispatchers[0]);
            AirplaneInterface apI = new AirplaneInterface();
            while (true)
            {
                for (int i = 1; i < dispatchers.Count; i++)
                {
                    try
                    {
                        airplane.DispChangeTimer.Elapsed += (source, e) => AirFlight(source, e, airplane, dispatchers[i]);
                        airplane.DispChangeTimer.Enabled = true;

                        airplane.HeightChange += dispatchers[i].Check;
                        airplane.SpeedChange += dispatchers[i].Check;
                        airplane.Fly();
                        apI.PrintAirplaneInfo(airplane);
                    }
                    catch (AirplaneCrashedException)
                    {
                        Console.WriteLine("Airplane crashed");
                    }
                }
            }
        }
            
        static void AirFlight(Object source, System.Timers.ElapsedEventArgs e, Airplane airplane, Dispatcher dispatcher)
        {
            airplane.AddDisp(dispatcher);
        }
    }
}
