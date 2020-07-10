using System;
using ConsoleManagement;
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

            IGameScreen humanGameScreen = new GameScreen();
            IGameScreen computerGameScreen = new GameScreen();
            view.SetObservedGameScreen(humanGameScreen);

            view.StartMessage();

            var option = Input.Read();
            switch (Input.GetOptionType(option))
            {
                case Input.OptionType.Menu:
                    Console.WriteLine("Chosen menu option");
                    view.Update();
                    break;
                case Input.OptionType.Game:
                    Console.WriteLine("Chosen game option");
                    view.Update();
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