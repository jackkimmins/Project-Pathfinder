using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Pathfinder
{
    public class Cell : Coordinate
    {
        public Cell(int x = 0, int y = 0) : base(x, y) { }
		public double Cost { get; set; }
		public int Distance { get; set; }
		public double CostDistance => Cost + Distance;
		public Cell Parent { get; set; }

		//Sets the distance property between two coordinates using the Pythagorean theorem.
		public void SetDistance(int targetX, int targetY)
		{
			this.Distance = (int)Math.Sqrt(Math.Pow(this.X - targetX, 2) + Math.Pow(this.Y - targetY, 2));
		}
	}
}
