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
        public Queue<Dispatcher> Disps { get; set; }
        public int Speed { get; private set; }
        public int Height { get; private set; }
        public int Penalty_points { get; set; }
        public Airplane(Dispatcher disp)
        {
            Disps = new Queue<Dispatcher>();
            Speed = 0;
            Height = 0;
            Disps.Enqueue(disp);
            DispChangeTimer = new System.Timers.Timer();
            DispChangeTimer.Interval = 20000;
        }

        public void Fly()
        {
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
            else if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) && keyInfo.Key == ConsoleKey.UpArrow)
            {
                Speed += 150;
            }
            else if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) && keyInfo.Key == ConsoleKey.DownArrow)
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
            else if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) && keyInfo.Key == ConsoleKey.PageUp)
            {
                Height += 500;
            }
            else if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) && keyInfo.Key == ConsoleKey.PageDown)
            {
                if (Height - 500 >= 0)
                {
                    Height -= 500;
                }              
            }

            ParamChange?.Invoke(this);

        }
        public void AddDisp(Dispatcher disp)
        {
            Disps.Enqueue(disp);
            Disps.Dequeue();
        }

        public override string ToString()
        {
            return "s";
        }
    }

    class AirplaneInterface
    {
        public void PrintAirplaneInfo(Airplane airplane)
        {
            Console.WriteLine("\nHeight: " + airplane.Height);
            Console.WriteLine("Speed: " + airplane.Speed);
        }
    }

    public class AirplaneCrashedException : Exception
    {
        public AirplaneCrashedException() { }

        public AirplaneCrashedException(string message)
            : base(message) { }
    }
}
