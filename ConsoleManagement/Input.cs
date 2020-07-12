using System;
using System.Linq;

namespace ConsoleManagement
{
    /// <summary>
    /// Manages all input form user.
    /// </summary>
    public static class Input
    {
        public enum OptionType
        {
            Menu,
            Game,
            Error
        }

        /// <summary>
        /// Takes input from user. Checks if it is composed of letters and digits.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public static string Read(string prompt = null)
        {
            if(prompt != null)
                Console.Write(prompt + ": ");
            var input = Console.ReadLine();
            Validate(input);
            return input;
        }

        /// <summary>
        /// Takes input from user and checks if it is valid coordinate.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks if input is composed of letters and digits.
        /// </summary>
        /// <param name="input"></param>
        private static void Validate(string input)
        {
            if (input.All(Char.IsLetterOrDigit) == false)
                throw new ArgumentException();
        }

        /// <summary>
        /// Determines type of command. Command could be option (OptionType.Menu), or coordinate(OptionType.Game).
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
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
