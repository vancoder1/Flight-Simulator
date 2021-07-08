using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using airplane;
using dispatcher;

namespace Flight_Simulator
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dispatcher> dispatchers = new List<Dispatcher>() { 
                new Dispatcher("L"),
                new Dispatcher("")
            };
            Airplane airplane = new Airplane(dispatchers[0]);
            

            for (int i = 1; i < dispatchers.Count; i++)
            {
                airplane.AddDisp(dispatchers[i]);
            }
        }
    }
}
