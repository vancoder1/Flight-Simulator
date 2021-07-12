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
            bool flag = true;
            int counter = 0;
            do
            {
                try
                {
                    airplane.DispChangeTimer.Elapsed += (source, e) => AirFlight(source, e, 
                        airplane, dispatchers, counter);
                    airplane.DispChangeTimer.Start();

                    airplane.ParamChange += dispatchers[counter].Check;
                    airplane.Fly();
                    apI.PrintAirplaneInfo(airplane);
                }
                catch (AirplaneCrashedException)
                {
                    Console.WriteLine("Airplane crashed");
                    Console.WriteLine("Game ended");
                    flag = false;
                }                
            } while (flag);
            
        }            
        static void AirFlight(Object source, System.Timers.ElapsedEventArgs e, Airplane airplane,
            List<Dispatcher> dispatchers, int counter)
        {
            if (counter >= dispatchers.Count)
            {
                counter = -1;
                return;
            }
            counter++;
            airplane.AddDisp(dispatchers[counter]);
        }
    }
}
