using System;
using ConsoleManagement;
using Game;
using Logic;
using LogicInterfaces;

namespace Battleships
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var menu = new Menu();
            var view = new View();

            IGameScreen humanGameScreen = new GameScreen();     //todo change to player list
            IGameScreen computerGameScreen = new GameScreen();
            view.SetObservedGameScreen(humanGameScreen);

            view.Refresh();

            var command = "";
            command = ReadValidInput(command, view);

            GameLoop(command, view);
        }

        private static void GameLoop(string command, View view)
        {
            switch (Input.GetOptionType(command))
            {
                case Input.OptionType.Menu:
                    Console.WriteLine("Chosen menu option");
                    view.Refresh();
                    break;
                case Input.OptionType.Game:
                    Console.WriteLine("Chosen game option");
                    view.Refresh();
                    break;
                case Input.OptionType.Error:
                    Console.WriteLine("Error");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static string ReadValidInput(string command, View view)
        {
            try
            {
                command = Input.Read();
            }
            catch (ArgumentException e)
            {
                Data.Message = "Wrong command, type again";
                view.Refresh();
            }

            return command;
        }
    }
}