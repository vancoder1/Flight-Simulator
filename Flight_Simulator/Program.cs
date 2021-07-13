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
            int counter = 0;
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

            airplane.ParamChange += dispatchers[counter].Check;
            airplane.DispChangeTimer.Elapsed += (source, e) => AirFlight(source, e,
                        airplane, dispatchers, ref counter);
            airplane.DispChangeTimer.Start();           
            do
            {
                try
                {
                    if (airplane.DispChangeTimer.Enabled == false)
                    {                       
                        apI.PrintEndOfGame(airplane);
                        flag = false;
                        break;
                    }                    
                    else if (counter < dispatchers.Count)
                    {
                        airplane.Fly();
                        apI.PrintAirplaneInfo(airplane, dispatchers[counter]);
                        Console.WriteLine("\t" + dispatchers[counter].Check(airplane));
                    }                    
                }
                catch (AirplaneCrashedException)
                {
                    Console.WriteLine("Airplane crashed");
                    apI.PrintEndOfGame(airplane, false);
                    flag = false;
                    break;
                }                
            } while (flag);
            
        }            
        static void AirFlight(Object source, System.Timers.ElapsedEventArgs e, Airplane airplane,
            List<Dispatcher> dispatchers, ref int counter)
        {
            if (counter < dispatchers.Count)
            {
                counter++;
                airplane.AddDisp(dispatchers[counter]);
                return;
            }
            else
            {
                airplane.DispChangeTimer.Stop();
            }
        }
    }
}
