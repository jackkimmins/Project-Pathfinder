using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Pathfinder
{
    public class Pathfinder
    {
        //Checks if the coordinate is the most optimal path.
        private void CheckCoordinate(ref Map map, ref double lowestF, ref Coordinate temp, Coordinate coord, ref Coordinate next)
        {
            temp.G = coord.G + 1;
            temp.H = Math.Abs(temp.X - map.End.X) + Math.Abs(temp.Y - map.End.Y);
            temp.F = temp.G + temp.H;
            if (temp.F < lowestF)
            {
                lowestF = temp.F;
                next = temp;
            }
        }

        //Finds the next coordinate to move to.
        private Coordinate FindNext(Map map, List<Coordinate> path)
        {
            Coordinate next = new Coordinate(map.Terrain.Size);
            double lowestF = int.MaxValue;

            foreach (Coordinate coord in path)
            {
                int x = coord.X;
                int y = coord.Y;

                //Check up
                if (y - 1 >= 0 && map.Terrain.MAP[x][y - 1] == 0)
                {
                    Coordinate temp = new Coordinate(x, y - 1);
                    CheckCoordinate(ref map, ref lowestF, ref temp, coord, ref next);
                }

                //Check down
                if (y + 1 < map.Terrain.Size && map.Terrain.MAP[x][y + 1] == 0)
                {
                    Coordinate temp = new Coordinate(x, y + 1);
                    CheckCoordinate(ref map, ref lowestF, ref temp, coord, ref next);
                }

                //Check left
                if (x - 1 >= 0 && map.Terrain.MAP[x - 1][y] == 0)
                {
                    Coordinate temp = new Coordinate(x - 1, y);
                    CheckCoordinate(ref map, ref lowestF, ref temp, coord, ref next);
                }

                //Check right
                if (x + 1 < map.Terrain.Size && map.Terrain.MAP[x + 1][y] == 0)
                {
                    Coordinate temp = new Coordinate(x + 1, y);
                    CheckCoordinate(ref map, ref lowestF, ref temp, coord, ref next);
                }

                //Check up-left
                if (x - 1 >= 0 && y - 1 >= 0 && map.Terrain.MAP[x - 1][y - 1] == 0)
                {
                    Coordinate temp = new Coordinate(x - 1, y - 1);
                    CheckCoordinate(ref map, ref lowestF, ref temp, coord, ref next);
                }

                //Check up-right
                if (x + 1 < map.Terrain.Size && y - 1 >= 0 && map.Terrain.MAP[x + 1][y - 1] == 0)
                {
                    Coordinate temp = new Coordinate(x + 1, y - 1);
                    CheckCoordinate(ref map, ref lowestF, ref temp, coord, ref next);
                }

                //Check down-left
                if (x - 1 >= 0 && y + 1 < map.Terrain.Size && map.Terrain.MAP[x - 1][y + 1] == 0)
                {
                    Coordinate temp = new Coordinate(x - 1, y + 1);
                    CheckCoordinate(ref map, ref lowestF, ref temp, coord, ref next);
                }

                //Check down-right
                if (x + 1 < map.Terrain.Size && y + 1 < map.Terrain.Size && map.Terrain.MAP[x + 1][y + 1] == 0)
                {
                    Coordinate temp = new Coordinate(x + 1, y + 1);
                    CheckCoordinate(ref map, ref lowestF, ref temp, coord, ref next);
                }

            }

            return next;
        }

        //Display map with start, end and path.
        public void DisplayPathOnMap(Map map, List<Coordinate> path)
        {
            AddPathToMap(map, path);
            
            Console.Clear();
            map.DisplayMap();

            //Sleep for 1 second
            System.Threading.Thread.Sleep(500);
        }

        //Adds the path steps to the map.
        private void AddPathToMap(Map map, List<Coordinate> path)
        {
            foreach (Coordinate coord in path)
            {
                map.Terrain.MAP[coord.X][coord.Y] = 2;
            }
        }

        //This method is used to find the path from start to end.
        public void CalculatePath(Map map)
        {
            Console.WriteLine("Start: " + map.Start.X + ", " + map.Start.Y);
            Console.WriteLine("End: " + map.End.X + ", " + map.End.Y);

            List<Coordinate> path = new List<Coordinate>();
            path.Add(map.Start);

            //While the end coordinate is not in the path
            while (true)
            {
                //Find the next coordinate in the path
                Coordinate next = FindNext(map, path);

                next.Output();
                AddPathToMap(map, path);
                //DisplayPathOnMap(map, path);

                if (next.X == map.End.X && next.Y == map.End.Y)
                {
                    Console.WriteLine("Found path!");
                    DisplayPathOnMap(map, path);
                    break;
                }

                //Add the next coordinate to the path
                path.Add(next);
            }
        }
    }
}