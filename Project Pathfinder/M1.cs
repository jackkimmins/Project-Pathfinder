using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Pathfinder
{
    class M1 : Pathfinder
    {
        //Checks if the coordinate is the most optimal path.
        private void CheckCoordinate(ref double lowestF, ref Coordinate temp, Coordinate coord, ref Coordinate next)
        {
            temp.G = coord.G + 1;
            temp.H = Math.Abs(temp.X - Map.End.X) + Math.Abs(temp.Y - Map.End.Y);
            temp.F = temp.G + temp.H;
            if (temp.F < lowestF)
            {
                lowestF = temp.F;
                next = temp;
            }
        }

        //Finds the next coordinate to move to.
        private Coordinate FindNext()
        {
            Coordinate next = new Coordinate(Map.Terrain.Size);
            double lowestF = int.MaxValue;

            foreach (Coordinate coord in Path)
            {
                int x = coord.X;
                int y = coord.Y;

                //Check up
                if (y - 1 >= 0 && Map.Terrain.MAP[x][y - 1] == 0)
                {
                    Coordinate temp = new Coordinate(x, y - 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check down
                if (y + 1 < Map.Terrain.Size && Map.Terrain.MAP[x][y + 1] == 0)
                {
                    Coordinate temp = new Coordinate(x, y + 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check left
                if (x - 1 >= 0 && Map.Terrain.MAP[x - 1][y] == 0)
                {
                    Coordinate temp = new Coordinate(x - 1, y);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check right
                if (x + 1 < Map.Terrain.Size && Map.Terrain.MAP[x + 1][y] == 0)
                {
                    Coordinate temp = new Coordinate(x + 1, y);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check up-left
                if (x - 1 >= 0 && y - 1 >= 0 && Map.Terrain.MAP[x - 1][y - 1] == 0)
                {
                    Coordinate temp = new Coordinate(x - 1, y - 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check up-right
                if (x + 1 < Map.Terrain.Size && y - 1 >= 0 && Map.Terrain.MAP[x + 1][y - 1] == 0)
                {
                    Coordinate temp = new Coordinate(x + 1, y - 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check down-left
                if (x - 1 >= 0 && y + 1 < Map.Terrain.Size && Map.Terrain.MAP[x - 1][y + 1] == 0)
                {
                    Coordinate temp = new Coordinate(x - 1, y + 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check down-right
                if (x + 1 < Map.Terrain.Size && y + 1 < Map.Terrain.Size && Map.Terrain.MAP[x + 1][y + 1] == 0)
                {
                    Coordinate temp = new Coordinate(x + 1, y + 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

            }

            return next;
        }

        public M1(Map map) : base(map)
        {

        }

        //This method is used to find the path from start to end.
        public override void CalculatePath()
        {
            Console.WriteLine("Start: " + Map.Start.X + ", " + Map.Start.Y);
            Console.WriteLine("End: " + Map.End.X + ", " + Map.End.Y);

            Path.Add(Map.Start);

            //While the end coordinate is not in the path
            while (true)
            {
                //Find the next coordinate in the path
                Coordinate next = FindNext();

                next.Output();
                AddStepsToMap();
                //DisplayPathOnMap(map, path);

                if (next.X == Map.End.X && next.Y == Map.End.Y)
                {
                    Console.WriteLine("Found path!");
                    DisplayPathOnMap();
                    break;
                }

                //Add the next coordinate to the path
                Path.Add(next);
            }
        }
    }
}
