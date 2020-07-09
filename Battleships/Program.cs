using System;
using ConsoleManagement;

namespace Battleships
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var menu = new Menu();

            var option = Input.Read();
            switch (Input.GetOptionType(option))
            {
                case Input.OptionType.Menu:
                    Console.WriteLine("Chosen menu option");
                    break;
                case Input.OptionType.Game:
                    Console.WriteLine("Chosen game option");
                    break;
                case Input.OptionType.Error:
                    Console.WriteLine("Error");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}