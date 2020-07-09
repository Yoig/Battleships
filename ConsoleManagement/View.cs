using System;

namespace ConsoleManagement
{
    public class View
    {
        public void Update()
        {
            Console.WriteLine();
            Console.WriteLine();
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}