using System;

namespace ConsoleManagement
{
    public static class Input
    {

        public static string Read(string prompt)
        {
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            Validate(input);
            return input;
        }

        private static void Validate(string input)
        {
        }
    }
}
