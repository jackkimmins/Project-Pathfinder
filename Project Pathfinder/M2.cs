using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Pathfinder
{
    class M2 : Pathfinder
    {
        public M2(Map map) : base(map) {}

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
        private Coordinate FindNext(ref Map tempMap, List<Coordinate> path)
        {
            Coordinate next = new Coordinate(tempMap.Terrain.Size);
            double lowestF = int.MaxValue;

            foreach (Coordinate coord in path)
            {
                int x = coord.X;
                int y = coord.Y;

                //Check up
                if (y - 1 >= 0 && tempMap.Terrain.MAP[x][y - 1] == 0)
                {
                    Coordinate temp = new Coordinate(x, y - 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check down
                if (y + 1 < tempMap.Terrain.Size && tempMap.Terrain.MAP[x][y + 1] == 0)
                {
                    Coordinate temp = new Coordinate(x, y + 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check left
                if (x - 1 >= 0 && tempMap.Terrain.MAP[x - 1][y] == 0)
                {
                    Coordinate temp = new Coordinate(x - 1, y);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check right
                if (x + 1 < tempMap.Terrain.Size && tempMap.Terrain.MAP[x + 1][y] == 0)
                {
                    Coordinate temp = new Coordinate(x + 1, y);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check up-left
                if (x - 1 >= 0 && y - 1 >= 0 && tempMap.Terrain.MAP[x - 1][y - 1] == 0)
                {
                    Coordinate temp = new Coordinate(x - 1, y - 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check up-right
                if (x + 1 < tempMap.Terrain.Size && y - 1 >= 0 && tempMap.Terrain.MAP[x + 1][y - 1] == 0)
                {
                    Coordinate temp = new Coordinate(x + 1, y - 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check down-left
                if (x - 1 >= 0 && y + 1 < tempMap.Terrain.Size && tempMap.Terrain.MAP[x - 1][y + 1] == 0)
                {
                    Coordinate temp = new Coordinate(x - 1, y + 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }

                //Check down-right
                if (x + 1 < tempMap.Terrain.Size && y + 1 < tempMap.Terrain.Size && tempMap.Terrain.MAP[x + 1][y + 1] == 0)
                {
                    Coordinate temp = new Coordinate(x + 1, y + 1);
                    CheckCoordinate(ref lowestF, ref temp, coord, ref next);
                }
            }

            return next;
        }


        private List<List<Coordinate>> AllPossiblePaths()
        {
            List<List<Coordinate>> paths = new List<List<Coordinate>>();

            foreach (Coordinate coord in Path.Last().Neighbors(Map.Terrain.Size))
            {
                //Check that the coord is 0
                if (Map.Terrain.MAP[coord.X][coord.Y] != 0)
                {
                    continue;
                }

                Map tempMap = Map;

                //Loop over tempMap.Terrain.MAP and set all the 2 coordinates to 0
                // for (int i = 0; i < tempMap.Terrain.Size; i++)
                // {
                //     for (int j = 0; j < tempMap.Terrain.Size; j++)
                //     {
                //         if (tempMap.Terrain.MAP[i][j] == 2)
                //         {
                //             tempMap.Terrain.MAP[i][j] = 0;
                //         }
                //     }
                // }

                List<Coordinate> path = new List<Coordinate>();
                path = Path;
                path.Add(coord);
                tempMap.Terrain.MAP[coord.X][coord.Y] = 2;


                while (path.Count < tempMap.Terrain.Size * tempMap.Terrain.Size)
                {  
                    Coordinate next = FindNext(ref tempMap, path);

                    if (next.X == tempMap.End.X && next.Y == tempMap.End.Y)
                    {
                        Console.WriteLine("Found path in " + path.Count + " steps.");
                        paths.Add(path);
                        break;
                    }

                    tempMap.Terrain.MAP[next.X][next.Y] = 2;

                    Console.Clear();
                    tempMap.DisplayMap();

                    Console.WriteLine("Naighbor: ");
                    coord.Output();
                    Console.WriteLine("--------------------");
                    Console.WriteLine("Current Coord: ");
                    next.Output();
                    Console.WriteLine("End Coord: ");
                    Map.End.Output();

                    Console.ReadKey();

                    path.Add(next);
                }
            }

            return paths;
        }

        private Coordinate GetNextStep()
        {
            List<List<Coordinate>> paths = AllPossiblePaths();

            List<Coordinate> shortestPath = paths.OrderBy(x => x.Count).First();

            // foreach (Coordinate coord in shortestPath)
            // {
            //     coord.Output();
            // }

            // Console.WriteLine("Outputting: ");
            // Path.Last().Output();

            //Get the next coordinate in the shortest path after the current coordinate.
            Coordinate next = shortestPath.SkipWhile(x => x.X != Path.Last().X || x.Y != Path.Last().Y).Skip(1).First();

            // Console.WriteLine("Outputting 2: ");
            // next.Output();

            return next;
        }

        public override void CalculatePath()
        {
            var data = Map.Start.Neighbors(Map.Terrain.Size);

            Path.Add(Map.Start);
            

            // 

            while (Path.Count < Map.Terrain.Size * Map.Terrain.Size)
            {
                //Get the next step
                Coordinate nextStep = GetNextStep();

                nextStep.Output();
                AddStepsToMap();
                // DisplayPathOnMap();

                foreach (Coordinate coord in Path)
                {
                    coord.Output();
                }

                //If the next step is the end, we're done
                if (nextStep.X == Map.End.X && nextStep.Y == Map.End.Y)
                {
                    Console.WriteLine("Found Final Path!");
                    DisplayPathOnMap();
                    break;
                }

                //Add the next step to the path
                Path.Add(nextStep);
            }
        }
    }
}