using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformy_Lab10
{
    class Engine : IComparable
    {
        public double Displacement { get; set; }
        public double Horsepower { get; set; }
        public string Model { get; set; }

        public Engine() { }

        public Engine(double displacement, double horsepower, string model)
        {
            Displacement = displacement;
            Horsepower = horsepower;
            Model = model;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null) return 1;
            if (obj.GetType() != typeof(Engine)) return 1;
            return Horsepower.CompareTo(((Engine)obj).Horsepower);
        }
    }
}
