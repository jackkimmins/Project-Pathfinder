using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Pathfinder
{
    public class Terrain
    {
        public int Seed { get; }
        public List<List<int>> MAP { get; }
        public int Size { get; }
        public bool Obstacles { get; }

        public Terrain(int seed = 0, bool obstacles = false, int size = 32)
        {
            if (seed == 0)
            {
                Random rndSeed = new Random();
                this.Seed = rndSeed.Next(1, 100000);
            }
            else
            {
                this.Seed = seed;
            }

            this.Obstacles = obstacles;
            this.Size = size;

            this.MAP = new List<List<int>>();
        }

        //Create a map with the option to add obstacles
        public void Generate()
        {
            this.MAP.Clear();

            Random rnd = new Random(this.Seed);
            for (int i = 0; i < this.Size; i++)
            {
                List<int> row = new List<int>();
                for (int j = 0; j < this.Size; j++)
                {
                    if (this.Obstacles)
                    {
                        //10% chance of being an obstacle
                        if (rnd.Next(0, 10) == 0)
                        {
                            row.Add(1);
                        }
                        else
                        {
                            row.Add(0);
                        }
                    }
                    else
                    {
                        row.Add(0);
                    }
                }
                
                this.MAP.Add(row);
            }
        }
    }
}
