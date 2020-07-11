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
            var view = new View();

            DefineBattleships();

            IPlayer humanPlayer = new Human();
            IPlayer computerPlayer = new Computer();
            SetOpponents(humanPlayer, computerPlayer);
            Data.Players.Enqueue(humanPlayer);
            Data.Players.Enqueue(computerPlayer);

            IGameScreen humanGameScreen = new GameScreen();
            IGameScreen computerGameScreen = new GameScreen();
            view.SetObservedGameScreen(computerGameScreen);

            view.Refresh();

            GameLoop(view, menu);
        }

        private static void DefineBattleships()
        {
            Rules.Battleships.Add("Carrier", 4);
            Rules.Battleships.Add("Cruiser", 3);
            Rules.Battleships.Add("Destroyer", 2);
            Rules.Battleships.Add("Submarine", 1);
        }

        private static void GameLoop(View view, Menu menu)
        {
            while (Data.State != Data.GameState.Ended)
            {
                if (Data.Players.Peek() is Human)
                {
                    var command = ReadValidInput(view);
                    switch (Input.GetOptionType(command))
                    {
                        case Input.OptionType.Menu:
                            menu.Option(command);
                            break;
                        case Input.OptionType.Game:
                            ManageTurn(view, command);
                            break;
                        case Input.OptionType.Error:
                            Data.Message = Data.PredefinedMessages.WrongInput;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                {
                    ManageTurn(view, "");
                }
                view.Refresh();
            }
        }

        private static void ManageTurn(View view, string command)
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

            view.Refresh();
        }

        private static void SetOpponents(IPlayer humanPlayer, IPlayer computerPlayer)
        {
            humanPlayer.SetOpponent(computerPlayer);
            computerPlayer.SetOpponent(humanPlayer);
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