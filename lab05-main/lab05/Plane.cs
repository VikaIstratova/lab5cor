using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_6___2__
{
    public class Plane
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Producer { get; set; }
        public int Seats { get; set; }
        public double Max_speed { get; set; }
        public double Time { get; set; }
        public bool Toilet { get; set; }
        public bool Duty_free { get; set; }

        public double GetDistance()

        {
            return Max_speed / Time;
        }

        public Plane()
        {

        }

        public Plane(string name, string country, string producer, int seats, double max_speed, double time, bool toilet, bool duty_free)
        {
            {
                Name = name;
                Country = country;
                Producer = producer;
                Seats = seats;
                Max_speed = max_speed;
                Time = time;
                Toilet = toilet;
                Duty_free = duty_free;
            }
        }
    }
}
