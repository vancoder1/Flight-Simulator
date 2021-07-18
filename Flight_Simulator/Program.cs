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

            Airplane airplane = new Airplane(dispatchers[counter]);

            airplane.AddDisp(dispatchers[counter]);
            AirplaneInterface apI = new AirplaneInterface();
            bool flag = true;
            
            airplane.DispChangeTimer.Elapsed += (source, e) => AirFlight(source, e,
                        airplane, dispatchers, ref counter);
            airplane.DispChangeTimer.Elapsed += (source, e) => apI.PrintAirplaneInfo(airplane, dispatchers[counter]);
            airplane.DispChangeTimer.Enabled = true;
            airplane.ParamChange += dispatchers[counter].Check;
            Console.WriteLine("Start of the game");
            do
            {
                try
                {
                    if (airplane.DispChangeTimer.Enabled == false)
                    {
                        flag = false;
                        return;
                    }
                    airplane.Fly();
                    if (airplane.DispChangeTimer.Enabled == true)
                    {
                        apI.PrintAirplaneInfo(airplane, dispatchers[counter]);
                        Console.WriteLine("\t" + dispatchers[counter].Check(airplane));
                    }
                }
                catch (AirplaneCrashedException)
                {
                    Console.Clear();
                    Console.WriteLine("\tAirplane crashed");
                    apI.PrintEndOfGame(airplane, false);
                    return;
                }
                catch (UnfitToFlyException)
                {
                    Console.Clear();
                    Console.WriteLine("Unfit to fly");
                    apI.PrintEndOfGame(airplane, false);
                    return;
                }
                catch (ArgumentOutOfRangeException)
                {
                    flag = false;
                    break;
                }
            } while (flag);
            
        }            
        static void AirFlight(Object source, System.Timers.ElapsedEventArgs e, Airplane airplane,
            List<Dispatcher> dispatchers, ref int counter)
        {
            counter++;
            if (counter < dispatchers.Count)
            {                
                airplane.AddDisp(dispatchers[counter]);
                dispatchers[counter].Check(airplane);
                return;
            }
            else
            {
                AirplaneInterface apI = new AirplaneInterface();
                apI.PrintEndOfGame(airplane, true);
                airplane.DispChangeTimer.Enabled = false;
            }
        }
    }
}
