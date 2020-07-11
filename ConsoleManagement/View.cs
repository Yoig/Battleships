using System;
using System.Buffers;
using Common;
using Game;
using Logic;
using LogicInterfaces;

namespace ConsoleManagement
{
    public class View
    {
        public void SetObservedGameScreen(IGameScreen gameScreen)
        {
            _observedGameScreen = gameScreen;
        }

        public void Refresh()
        {
            Console.Clear();

            switch (Data.State)
            {
                case Data.GameState.NotStarted:
                    ShowStartView();
                    break;
                case Data.GameState.Ongoing:
                    ShowOngoingView();
                    break;
                case Data.GameState.Ended:
                    ShowEndView();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ShowStartView()
        {
            Console.WriteLine("Welcome to battleships!");
            Console.WriteLine(Data.Message);
            Console.Write("To start game type ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("start");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ShowOngoingView()
        {
            Console.WriteLine(Data.Message);
            Console.SetCursorPosition(OwnBoardPositionX + 5, MessageSpaceY);
            Console.WriteLine("Your board");
            Console.SetCursorPosition(OpponentBoardPositionX + 3, MessageSpaceY);
            Console.WriteLine("Opponent board");

            PrepareBorders(OwnBoardPositionX);
            PrepareBorders(OpponentBoardPositionX);

            PrepareInterior(OwnBoardPositionX, _observedGameScreen.OwnBoard);
            PrepareInterior(OpponentBoardPositionX, _observedGameScreen.OpponentBoard);

            Console.SetCursorPosition(0, CommandPositionY);
        }

        public void ShowEndView()
        {
            Console.WriteLine("Game has ended!");
            Console.WriteLine(Data.Winner + "has won!");
        }

        private void PrepareInterior(int boardPosition, IGameboard board)
        {
            for (var y = 0; y < 10; y++)
            {
                Console.SetCursorPosition(boardPosition, MessageSpaceY + 2 + y);
                for (var x = 0; x < 10; x++)
                {
                    DrawField(board.RawBoard, x, y);
                    Console.Write(" ");
                }
            }
        }

        private static void DrawField(Rules.FieldState[,] rawBoard, int x, int y)
        {
            switch (rawBoard[x, y])
            {
                case Rules.FieldState.Battleship:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write('#');
                    break;
                case Rules.FieldState.Empty:
                    break;
                case Rules.FieldState.Mishit:
                    Console.Write('-');
                    break;
                case Rules.FieldState.Hit:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write('H');
                    break;
                case Rules.FieldState.Sunken:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write('0');
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void PrepareBorders(int boardPositionX)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            for (var j = 0; j < 10; j++)
            {
                Console.SetCursorPosition(boardPositionX + 2 * j, MessageSpaceY + 1);
                Console.Write(j);
                Console.Write(" ");

                Console.SetCursorPosition(boardPositionX - 2, MessageSpaceY + 2 + j);
                Console.Write((char)('A' + j));
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        private const int OwnBoardPositionX = 3;
        private const int OpponentBoardPositionX = 30;
        private const int MessageSpaceY = 2;
        private const int CommandPositionY = MessageSpaceY + 2 + 10 + 1;

        private IGameScreen _observedGameScreen;
    }
}