using System;
using System.Threading;
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

            GameLoop(view, menu);
        }

        private static void GameLoop(View view, Menu menu)
        {
            while (Data.State != Data.GameState.Ended)
            {
                var command = ReadValidInput(view);
                switch (Input.GetOptionType(command))
                {
                    case Input.OptionType.Menu:
                        menu.Option(command);
                        break;
                    case Input.OptionType.Game:
                        if (Data.State != Data.GameState.Ongoing)
                            break;
                        break;
                    case Input.OptionType.Error:
                        Data.Message = Data.PredefinedMessages.WrongInput;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                view.Refresh();
            }
        }

        private static string ReadValidInput(View view)
        {
            var command = "";
            try
            {
                command = Input.Read();
            }
            catch (ArgumentException e)
            {
                Data.Message = Data.PredefinedMessages.WrongInput;
                view.Refresh();
            }

            return command;
        }
    }
}