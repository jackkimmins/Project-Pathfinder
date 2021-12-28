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

        public Coordinate(int x = 0, int y = 0)
        {
            this.X = x;
            this.Y = y;

            this.G = 0;
            this.H = 0;
            this.F = 0;
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

        //Find the distance between two coordinates using the Pythagorean theorem.
        public int DistanceTo(Coordinate coord)
        {
            return (int)Math.Sqrt(Math.Pow(this.X - coord.X, 2) + Math.Pow(this.Y - coord.Y, 2));
        }

        //Gets the 8 surrounding neighbors of the current coordinate.
        public List<Coordinate> Neighbors(int gridSize)
        {
            List<Coordinate> neighbors = new List<Coordinate>();

            if (this.X > 0)
            {
                neighbors.Add(new Coordinate(this.X - 1, this.Y));
            }
            if (this.X < gridSize - 1)
            {
                neighbors.Add(new Coordinate(this.X + 1, this.Y));
            }
            if (this.Y > 0)
            {
                neighbors.Add(new Coordinate(this.X, this.Y - 1));
            }
            if (this.Y < gridSize - 1)
            {
                neighbors.Add(new Coordinate(this.X, this.Y + 1));
            }

            if (this.X > 0 && this.Y > 0)
            {
                neighbors.Add(new Coordinate(this.X - 1, this.Y - 1));
            }
            if (this.X < gridSize - 1 && this.Y > 0)
            {
                neighbors.Add(new Coordinate(this.X + 1, this.Y - 1));
            }
            if (this.X > 0 && this.Y < gridSize - 1)
            {
                neighbors.Add(new Coordinate(this.X - 1, this.Y + 1));
            }
            if (this.X < gridSize - 1 && this.Y < gridSize - 1)
            {
                neighbors.Add(new Coordinate(this.X + 1, this.Y + 1));
            }

            return neighbors;
        }
    }
}
