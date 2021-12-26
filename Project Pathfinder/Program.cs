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

                // M1 mark1 = new M1(map);
                // mark1.CalculatePath();

                M2 mark2 = new M2(map);
                mark2.CalculatePath();

                Console.WriteLine("Distance: " + map.Distance());

                Console.ReadKey();
            }
        }
    }
}
