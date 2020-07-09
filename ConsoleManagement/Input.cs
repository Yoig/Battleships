using System;
using System.ComponentModel.Design;
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
            Console.WriteLine(prompt);
            var input = Console.ReadLine();
            Validate(input);
            return input;
        }

        private static void Validate(string input)
        {
        }

        public static OptionType GetOptionType(string option)
        {
            static bool IsMenuType(string s) => s == "start" || s == "exit";
            static bool IsGameType(string s) => s[0] >= 'a' && s[0] <= 'j' && s[1] >= '0' && s[1] <= '9';

            option = option.ToLower();
            if (IsMenuType(option))
                return OptionType.Menu;

            if (IsGameType(option))
            { 
                return OptionType.Game;
            }

            return OptionType.Error;
        }
    }
}
