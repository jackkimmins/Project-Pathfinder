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
            this.Terrain = new Terrain(0, true, size);
            this.Start = new Coordinate(size);
            this.End = new Coordinate(size);
        }

        public void Generate()
        {
            this.Terrain.Generate();
        }

        private void GenHorsBorderLine(int length)
        {
            //Generate the horizontal border line according
            //to the length x 2 and the extra 2 for the vertical borders.
            for (int i = 0; i < (length * 2) + 2; i++)
                Console.Write("#");
            Console.WriteLine();
        }

        private void GenVertBorderLine(bool withGap = false)
        {
            Console.Write(withGap ? " #" : "#");
        }

        private void Write(char value, ConsoleColor colour = ConsoleColor.White)
        {
            //Console.ForegroundColor = (ConsoleColor)new Random().Next(1, 15);
            Console.ForegroundColor = colour;
            Console.Write(value);
            Console.ResetColor();
        }

        //Update the map at the given coordinate.
        public void UpdateMapAtPosition(Coordinate coord, char value)
        {
            this.Terrain.MAP[coord.X][coord.Y] = value;
        }

        //Display map with start and end points
        public void DisplayMap()
        {
            GenHorsBorderLine(this.Terrain.Size);

            for (int i = 0; i < this.Terrain.Size; i++)
            {
                for (int j = 0; j < this.Terrain.Size; j++)
                {
                    if (j == 0)
                    {
                        GenVertBorderLine();
                    }

                    if (this.Start.X == i && this.Start.Y == j)
                    {
                        Write('S', ConsoleColor.Yellow);
                    }
                    else if (this.End.X == i && this.End.Y == j)
                    {
                        Write('E', ConsoleColor.Yellow);
                    }
                    else
                    {

                        switch (this.Terrain.MAP[i][j])
                        {
                            case 0:
                                Write(' ', ConsoleColor.Black);
                                break;
                            case 2:
                                Write('*', ConsoleColor.White);
                                break;
                            default:
                                Write('X', ConsoleColor.DarkCyan);
                                break;
                        }

                        Console.Write(" ");
                    }

                    if (j == this.Terrain.Size - 1)
                    {
                        GenVertBorderLine(i == this.Start.X || i == this.End.X ? true : false);
                    }
                }

                Console.WriteLine();
            }
            
            GenHorsBorderLine(this.Terrain.Size);
        }

        public void RawOutput()
        {
            Console.Clear();
            for (int i = 0; i < this.Terrain.Size; i++)
            {
                for (int j = 0; j < this.Terrain.Size; j++)
                {
                    Console.Write(this.Terrain.MAP[i][j]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        //Gets the distance between the start and end points using Pythagoras
        public int Distance()
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
