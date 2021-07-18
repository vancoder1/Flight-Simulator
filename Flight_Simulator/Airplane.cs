using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using dispatcher;

namespace airplane
{
    delegate string StatusChangeHandler(Airplane airplane);
    class Airplane
    {
        public event StatusChangeHandler ParamChange;

        public Timer DispChangeTimer;
        private int Interval = 2000;
        public Queue<Dispatcher> Disps { get; set; }
        public int Speed { get; private set; }
        public int Height { get; private set; }
        public int Penalty_points { get; set; }
        public Airplane()
        {
            Disps = new Queue<Dispatcher>();
            Speed = 0;
            Height = 0;
            DispChangeTimer = new System.Timers.Timer();
            DispChangeTimer.Interval = Interval;
        }
        public Airplane(Dispatcher disp)
        {
            Disps = new Queue<Dispatcher>();
            Speed = 0;
            Height = 0;
            Disps.Enqueue(disp);
            DispChangeTimer = new System.Timers.Timer();
            DispChangeTimer.Interval = Interval;
        }

        public void Fly()
        {
            if (DispChangeTimer.Enabled == false)
            {
                return;
            }
            if (Disps.Count == 0)
            {
                Console.WriteLine("There are no dispatchers!");
                return;
            }
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.TreatControlCAsInput = true;

            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                Speed += 50;
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                if (Speed - 50 >= 0)
                {
                    Speed -= 50;
                }                
            }
            // Вместо SHIFT + UpArrow
            else if (keyInfo.Key == ConsoleKey.W)
            {
                Speed += 150;
                
            }
            // Вместо SHIFT + DownArrow
            else if (keyInfo.Key == ConsoleKey.S)
            {
                if (Speed - 150 >= 0)
                {
                    Speed -= 150;
                }
            }
            else if (keyInfo.Key == ConsoleKey.PageUp)
            {
                Height += 250;
            }
            else if (keyInfo.Key == ConsoleKey.PageDown)
            {
                if (Height - 250 >= 0)
                {
                    Height -= 250;
                }                
            }
            // Вместо SHIFT + PageUp
            else if (keyInfo.Key == ConsoleKey.Q)
            {
                Height += 500;
            }
            // Вместо SHIFT + PageDown
            else if (keyInfo.Key == ConsoleKey.A)
            {
                if (Height - 500 >= 0)
                {
                    Height -= 500;
                }              
            }

            if (DispChangeTimer.Enabled == true)
            {
                ParamChange?.Invoke(this);
            }          

        }
        public void AddDisp(Dispatcher disp)
        {
            Disps.Enqueue(disp);
            if (Disps.Count > 1)
            {
                Disps.Dequeue();
            }           
        }

        public override string ToString()
        {
            return "s";
        }
    }

    class AirplaneInterface
    {
        public void PrintAirplaneInfo(Airplane ap, Dispatcher disp)
        {
            Console.Clear();           
            Console.WriteLine("\nCurrent Height: " + ap.Height);
            Console.WriteLine("Current Speed: " + ap.Speed);
            Console.WriteLine($"\n{disp.Name} message: \n\tRecommended height: {disp.Recommended_Height}");
        }
        public void PrintEndOfGame(Airplane ap, bool clean = true)
        {
            if (clean == true)
            {
                Console.Clear();
            }
            Console.WriteLine("Press any key to exit\n");
            Console.WriteLine("\t\tGame ended");
            Console.WriteLine("\t\tPenalty points: " + ap.Penalty_points);
            Console.ReadKey();
        }
    }

    class AirplaneCrashedException : Exception
    {
        public AirplaneCrashedException() { }

        public AirplaneCrashedException(string message)
            : base(message) { }
    }
    class UnfitToFlyException : Exception
    {
        public UnfitToFlyException() { }

        public UnfitToFlyException(string message)
            : base(message) { }
    }
}
