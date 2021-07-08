using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using airplane;

namespace dispatcher
{
    class Dispatcher
    {
        public string Name { get; private set; }
        public int Weather_adjustments { get; private set; }
        public int Max_speed { get; private set; }
        public int Max_height { get; private set; }

        public Dispatcher(string name)
        {
            if (name.Length > 2)
            {
                Name = name;
            }
            else
            {
                throw new ArgumentException();
            }
            Random random = new Random();
            Weather_adjustments = random.Next(-200, 200);
            Max_speed = 1000;
        }
        public void Check(Airplane ap)
        {
            
        }
    }
}
