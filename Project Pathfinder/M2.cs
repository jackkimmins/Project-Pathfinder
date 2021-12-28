using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Pathfinder
{
    class M2 : Pathfinder
    {
        public M2(Map map) : base(map) { }

		//Get all the possible cells around the current cell.
		private static List<Cell> GetWalkableTiles(List<string> map, Cell currentCell, Cell targetCell)
		{
			List<Cell> possibleCells = new List<Cell>();

			//Top
			possibleCells.Add(new Cell { X = currentCell.X, Y = currentCell.Y - 1, Parent = currentCell, Cost = currentCell.Cost + 1 });

			//Bottom
			possibleCells.Add(new Cell { X = currentCell.X, Y = currentCell.Y + 1, Parent = currentCell, Cost = currentCell.Cost + 1 });

			//Left
			possibleCells.Add(new Cell { X = currentCell.X - 1, Y = currentCell.Y, Parent = currentCell, Cost = currentCell.Cost + 1 });

			//Right
			possibleCells.Add(new Cell { X = currentCell.X + 1, Y = currentCell.Y, Parent = currentCell, Cost = currentCell.Cost + 1 });

			//Top Left
			possibleCells.Add(new Cell { X = currentCell.X - 1, Y = currentCell.Y - 1, Parent = currentCell, Cost = currentCell.Cost + 1.4 });

			//Top Right
			possibleCells.Add(new Cell { X = currentCell.X + 1, Y = currentCell.Y - 1, Parent = currentCell, Cost = currentCell.Cost + 1.4 });

			//Bottom Left
			possibleCells.Add(new Cell { X = currentCell.X - 1, Y = currentCell.Y + 1, Parent = currentCell, Cost = currentCell.Cost + 1.4 });

			//Bottom Right
			possibleCells.Add(new Cell { X = currentCell.X + 1, Y = currentCell.Y + 1, Parent = currentCell, Cost = currentCell.Cost + 1.4 });

			//Gets the distance between the current cell and the target cell.
			possibleCells.ForEach(cell => cell.SetDistance(targetCell.X, targetCell.Y));

			var maxX = map.First().Length - 1;
			var maxY = map.Count - 1;

			//Removes all the cells that are out of the map.
			possibleCells.RemoveAll(cell => cell.X < 0 || cell.Y < 0 || cell.X > maxX || cell.Y > maxY);

			//Removes all the cells that are not walkable, not '0' or not '3'.
			possibleCells.RemoveAll(cell => map[cell.Y][cell.X] != '0' && map[cell.Y][cell.X] != '3');

			return possibleCells;
		}

		//Convert 'Map' into a list of strings.
		private List<string> LoadMap()
		{
			var map = new List<string>();

			for (int i = 0; i < Map.Terrain.Size; i++)
            {
				string row = "";

                for (int j = 0; j < Map.Terrain.Size; j++)
                {
					row += Map.Terrain.MAP[i][j].ToString();
                }

				map.Add(row);
            }

			return map;
		}

		//Display stats about the pathfinding algorithm.
		public void Stats()
		{
			Console.Write("Path Steps: " + Path.Count);
			Console.Write("      Distance: " + Map.Distance());
			Console.Write("      Seed: " + Map.Terrain.Seed);
		}

		public override void CalculatePath()
        {
			List<string> map = LoadMap();

			//Convert the start and end coordinates to Cell objects
			Cell start = new Cell(Map.Start.X, Map.Start.Y);
			Cell end = new Cell(Map.End.X, Map.End.Y);

			//Sets an estimate of the distance between the start and end points.
			start.SetDistance(end.X, end.Y);

			List<Cell> activeCells = new List<Cell>();
			List<Cell> visitedCells = new List<Cell>();
			activeCells.Add(start);

			while (activeCells.Any())
			{
				var checkCell = activeCells.OrderBy(x => x.CostDistance).First();

				// Check if we have reached the end cell.
				if (checkCell.X == end.X && checkCell.Y == end.Y)
				{
					Cell cell = checkCell;

					//Trace our steps back to the start and display the path.
					while (true)
					{
						Path.Add(new Coordinate(cell.X, cell.Y));

						cell = cell.Parent;

						if (cell == null)
						{
							AddStepsToMap();
							DisplayPathOnMap();
							return;
						}
					}
				}

				//Add the current cell to the visited list and remove it from the active list.
				visitedCells.Add(checkCell);
				activeCells.Remove(checkCell);

				var walkableCells = GetWalkableTiles(map, checkCell, end);

				foreach (var walkableCell in walkableCells)
				{
					//Skip if we have already visited this cell.
					if (visitedCells.Any(x => x.X == walkableCell.X && x.Y == walkableCell.Y))
						continue;

					//Check if there is a shorter path using this cell.
					if (activeCells.Any(x => x.X == walkableCell.X && x.Y == walkableCell.Y))
					{
						var existingTile = activeCells.First(x => x.X == walkableCell.X && x.Y == walkableCell.Y);
						if (existingTile.CostDistance > checkCell.CostDistance)
						{
							activeCells.Remove(existingTile);
							activeCells.Add(walkableCell);
						}
					}
					else
					{
						//Add new cell to the list.
						activeCells.Add(walkableCell);
					}
				}
			}

			DisplayPathOnMap();
			Console.WriteLine("No Path Found!");
		}
    }
}
