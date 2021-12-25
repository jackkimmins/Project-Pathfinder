using System;

namespace Project_Pathfinder
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Map map = new Map();
                map.Generate();

                // map.DisplayMap();

                Pathfinder finder = new Pathfinder();
                finder.CalculatePath(map);
                Console.WriteLine("Distance: " + map.Distance());

                Console.ReadKey();
            }
        }
    }
}
