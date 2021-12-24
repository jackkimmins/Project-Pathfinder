using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Pathfinder
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public double G { get; set; }
        public double H { get; set; }
        public double F { get; set; }

        public Coordinate Parent { get; set; }

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Coordinate(int gridSize)
        {
            Random rnd = new Random();
            this.X = rnd.Next(0, gridSize);
            this.Y = rnd.Next(0, gridSize);
        }

        public void Output()
        {
            Console.WriteLine("X: " + this.X + " Y: " + this.Y);
        }
    }
}
