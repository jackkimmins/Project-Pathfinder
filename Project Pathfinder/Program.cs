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

                M1 finder = new M1(map);
                finder.CalculatePath();
                Console.WriteLine("Distance: " + map.Distance());

                Console.ReadKey();
            }
        }
    }
}
