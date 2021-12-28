using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Pathfinder
{
    public abstract class Pathfinder
    {
        public List<Coordinate> Path = new List<Coordinate>();
        public Map Map { get; set; }

        protected Pathfinder(Map map)
        {
            Console.Title = "Pathfinder";
            this.Map = map;
        }

        protected void RemoveStepsFromMap()
        {
            foreach (Coordinate coord in Path)
            {
                Map.Terrain.MAP[coord.X][coord.Y] = 0;
            }
        }

        protected void AddStepsToMap()
        {
            foreach (Coordinate coord in Path)
            {
                Map.Terrain.MAP[coord.X][coord.Y] = 2;
            }
        }

        public void DisplayPathOnMap()
        {
            AddStepsToMap();
            
            Console.Clear();
            Map.DisplayMap();

            //Sleep for 1 second
            System.Threading.Thread.Sleep(500);
        }

        public abstract void CalculatePath();
    }
}