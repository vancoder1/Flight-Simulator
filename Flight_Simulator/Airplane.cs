using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dispatcher;

namespace airplane
{
    delegate void StatusCheck();
    class Airplane
    {
        public event StatusCheck SpeedChange;
        public event StatusCheck HeightChange;
        public Queue<Dispatcher> Disps { get; set; }
        public int Speed { get; private set; }
        public int Height { get; private set; }
        public Airplane(Dispatcher disp)
        {
            Speed = 0;
            Height = 0;
            Disps.Enqueue(disp);
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
                Speed -= 50;
            }
            else if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) && keyInfo.Key == ConsoleKey.UpArrow)
            {
                Speed += 150;
            }
            else if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) && keyInfo.Key == ConsoleKey.DownArrow)
            {
                Speed -= 150;
            }
            else if (keyInfo.Key == ConsoleKey.PageUp)
            {
                Height += 250;
            }
            else if (keyInfo.Key == ConsoleKey.PageDown)
            {
                Height -= 250;
            }
            else if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) && keyInfo.Key == ConsoleKey.PageUp)
            {
                Height += 500;
            }
            else if (keyInfo.Modifiers.HasFlag(ConsoleModifiers.Shift) && keyInfo.Key == ConsoleKey.PageDown)
            {
                Height -= 500;
            }
        }
        public void AddDisp(Dispatcher disp)
        {
            Disps.Enqueue(disp);
            Disps.Dequeue();
        }
    }
}
