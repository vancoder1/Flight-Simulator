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
        public int Weather_Adjustments { get; private set; }
        public int Recommended_Height  { get; private set; }
        public int Max_speed { get; private set; }
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
            Weather_Adjustments = random.Next(-200, 200);
            Max_speed = 1000;
        }
        public string Check(Airplane ap)
        {
            Recommended_Height = 7 * ap.Speed + Weather_Adjustments;
            if (+(ap.Height - Recommended_Height) > 300 && +(ap.Height - Recommended_Height) < 600)
            {
                ap.Penalty_points += 25;
            }
            else if (+(ap.Height - Recommended_Height) >= 600 && +(ap.Height - Recommended_Height) <= 1000)
            {
                ap.Penalty_points += 50;
            }
            else if (+(ap.Height - Recommended_Height) > 1000)
            {
                throw new AirplaneCrashedException();
            }
            else if (ap.Height > 0 && ap.Speed == 0)
            {
                throw new AirplaneCrashedException();
            }

            if (ap.Speed > Max_speed)
            {
                ap.Penalty_points += 100;
                return "Slow down airplane immediately";
            }
            return "It's ok";
        }
    }
}
