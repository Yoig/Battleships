using System;
using System.Linq;

namespace ConsoleManagement
{
    public static class Input
    {
        public enum OptionType
        {
            Menu,
            Game,
            Error
        }

        public static string Read(string prompt = null)
        {
            if(prompt != null)
                Console.Write(prompt + ": ");
            var input = Console.ReadLine();
            Validate(input);
            return input;
        }

        public static string ReadCoordinate(string prompt = null)
        {
            if (prompt != null)
                Console.Write(prompt + ": ");
            var input = Console.ReadLine();
            Validate(input);
            if (IsGameType(input) == false)
                throw new ArgumentException();
            return input;
        }

        private static void Validate(string input)
        {
            if (input.All(Char.IsLetterOrDigit) == false)
                throw new ArgumentException();
        }

        public static OptionType GetOptionType(string option)
        {
            option = option.ToLower();

            if (IsMenuType(option))
                return OptionType.Menu;
            if (IsGameType(option))
                return OptionType.Game;
            return OptionType.Error;
        }

        private static bool IsMenuType(string s) => s == "start" || s == "exit" || s == "help" || s == "back";

        private static bool IsGameType(string s) => s.Length == 2 && s[0] >= 'a' && s[0] <= 'j' && s[1] >= '0' && s[1] <= '9';
    }
}
