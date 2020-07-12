using System;
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

            GameLoop(computerGameScreen, humanGameScreen);
        }

        private static void DefineBattleships()
        {
            Rules.Battleships.Add("Carrier", 4);
            Rules.Battleships.Add("Cruiser", 3);
            Rules.Battleships.Add("Destroyer", 2);
            Rules.Battleships.Add("Submarine", 1);
        }

        /// <summary>
        /// Invokes function in proper order. Manages turn sequence.
        /// </summary>
        /// <param name="gameScreenComputer"></param>
        /// <param name="gameScreenHuman"></param>
        private static void GameLoop(IGameScreen gameScreenComputer, IGameScreen gameScreenHuman)
        {
            while (Data.State != Data.GameState.Ended)
            {
                if (Data.Players.Peek() is Human)
                {
                    if (Data.State == Data.GameState.Ongoing)
                    {
                        Data.MessageSecondLine = "It's your turn!";
                        View.Refresh();
                    }
                    var command = ReadValidInput();
                    switch (Input.GetOptionType(command))
                    {
                        case Input.OptionType.Menu:
                            Menu.Option(command);
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

        /// <summary>
        /// Manages specific turn. Updates view with messages what is currently going on.
        /// </summary>
        /// <param name="command"></param>
        private static void ManageTurn(string command)
        {
            if (Data.State != Data.GameState.Ongoing)
            {
                Data.MessageFirstLine = "Game has not yet began! To start it type start";
                return;
            }
            var outcome = Data.Players.Peek().PlayTurn(command);
            switch (outcome)
            {
                case Rules.FieldType.Mishit:
                    Data.MessageFirstLine = Data.Players.Peek() + " - Mishit!";
                    Data.Players.Enqueue(Data.Players.Dequeue());
                    break;
                case Rules.FieldType.Last:
                    Data.MessageFirstLine = "That was last battleship!";
                    View.Refresh();
                    Data.Winner = Data.Players.Peek();
                    Thread.Sleep(3000);
                    Menu.Exit();
                    break;
                case Rules.FieldType.Sunken:
                    Data.MessageFirstLine = Data.Players.Peek().Opponent + "'s battleship destroyed!";
                    break;
                case Rules.FieldType.Hit:
                    Data.MessageFirstLine = Data.Players.Peek().Opponent + "'s battleship hit!";
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

        /// <summary>
        /// Takes input from user and checks if it is valid.
        /// </summary>
        /// <returns></returns>
        private static string ReadValidInput()
        {
            var command = "";
            try
            {
                command = Input.Read();
            }
            catch (ArgumentException)
            {
                Data.MessageFirstLine = Data.PredefinedMessages.WrongInput;
                View.Refresh();
            }

            return command;
        }
    }
}