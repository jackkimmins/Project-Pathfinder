using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Pathfinder
{
    public class Map
    {
        public Coordinate Start { get; set; }
        public Coordinate End { get; set; }
        public Terrain Terrain { get; set; }

        public Map(int size = 32)
        {
            this.Terrain = new Terrain(true, size);
            this.Start = new Coordinate(size);
            this.End = new Coordinate(size);
        }

        public void Generate(int seed = 0)
        {
            this.Terrain.Generate(seed);
        }

        //Display map with start and end points
        public void DisplayMap()
        {
            Console.WriteLine("Map:");
            for (int i = 0; i < this.Terrain.Size; i++)
            {
                for (int j = 0; j < this.Terrain.Size; j++)
                {
                    if (this.Start.X == i && this.Start.Y == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("S");
                        Console.ResetColor();
                    }
                    else if (this.End.X == i && this.End.Y == j)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("E");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (this.Terrain.MAP[i][j] == 0)
                        {
                            Console.Write(" ");
                        }
                        else if (this.Terrain.MAP[i][j] == 2)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("*");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("X");
                        }

                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }
        }

        //Gets the distance between the start and end points using Pythagoras
        public int GetDistance()
        {
            return (int)Math.Sqrt(Math.Pow(this.Start.X - this.End.X, 2) + Math.Pow(this.Start.Y - this.End.Y, 2));
        }

        //Gets the distance between the start and specified coordinate using Pythagoras
        public int Distance(Coordinate c)
        {
            return (int)Math.Sqrt(Math.Pow(this.Start.X - c.X, 2) + Math.Pow(this.Start.Y - c.Y, 2));
        }
    }
}
