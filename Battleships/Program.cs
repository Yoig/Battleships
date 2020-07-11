using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;
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

            DefineBattleships();

            IGameScreen humanGameScreen = new GameScreen();
            IGameScreen computerGameScreen = new GameScreen();
            View.SetObservedGameScreen(humanGameScreen);

            IPlayer humanPlayer = new Human(humanGameScreen);
            IPlayer computerPlayer = new Computer(computerGameScreen);
            SetOpponents(humanPlayer, computerPlayer);
            Data.Players.Enqueue(humanPlayer);
            Data.Players.Enqueue(computerPlayer);

            View.Refresh();

            GameLoop(menu);
        }

        private static void DefineBattleships()
        {
            Rules.Battleships.Add("Carrier", 4);
            Rules.Battleships.Add("Cruiser", 3);
            Rules.Battleships.Add("Destroyer", 2);
            Rules.Battleships.Add("Submarine", 1);
        }

        private static void GameLoop(Menu menu)
        {
            while (Data.State != Data.GameState.Ended)
            {
                if (Data.Players.Peek() is Human)
                {
                    var command = ReadValidInput();
                    switch (Input.GetOptionType(command))
                    {
                        case Input.OptionType.Menu:
                            menu.Option(command);
                            break;
                        case Input.OptionType.Game:
                            ManageTurn(command);
                            break;
                        case Input.OptionType.Error:
                            Data.MessageFirstLine = Data.PredefinedMessages.WrongInput;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    ManageTurn("");
                }
                View.Refresh();
            }
        }

        private static void ManageTurn(string command)
        {
            if (Data.State != Data.GameState.Ongoing)
                return;
            var outcome = Data.Players.Peek().PlayTurn(command);
            switch (outcome)
            {
                case Rules.FieldState.Mishit:
                    Data.Players.Enqueue(Data.Players.Dequeue());
                    break;
                case Rules.FieldState.Last:
                    Data.State = Data.GameState.Ended;
                    Data.Winner = Data.Players.Peek();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            View.Refresh();
        }

        private static void SetOpponents(IPlayer humanPlayer, IPlayer computerPlayer)
        {
            humanPlayer.SetOpponent(computerPlayer);
            computerPlayer.SetOpponent(humanPlayer);
        }

        private static string ReadValidInput()
        {
            var command = "";
            try
            {
                command = Input.Read();
            }
            catch (ArgumentException e)
            {
                Data.MessageFirstLine = Data.PredefinedMessages.WrongInput;
                View.Refresh();
            }

            return command;
        }
    }
}