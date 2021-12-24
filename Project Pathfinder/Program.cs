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

                Pathfinder finder = new Pathfinder();
                finder.CalculatePath(map);
                Console.WriteLine("Distance: " + map.GetDistance());

                Console.ReadKey();
            }
        }
    }
}
