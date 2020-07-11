using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading;
using Microsoft.VisualBasic;

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
            if (prompt != null)
                Console.WriteLine(prompt);
            var input = Console.ReadLine();
            Validate(input);
            return input;
        }

        private static void Validate(string input)
        {
            if (input.All(Char.IsLetterOrDigit) == false)
                throw new ArgumentException();
        }

        public static OptionType GetOptionType(string option)
        {
            //todo rework to list of options and performing exist check
            static bool IsMenuType(string s) => s == "start" || s == "exit";
            static bool IsGameType(string s) => s.Length == 2 && s[0] >= 'a' && s[0] <= 'j' && s[1] >= '0' && s[1] <= '9';

            option = option.ToLower();

            if (IsMenuType(option))
                return OptionType.Menu;
            if (IsGameType(option))
                return OptionType.Game;
            return OptionType.Error;
        }
    }
}
