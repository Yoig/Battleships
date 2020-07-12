using System;
using System.Buffers;
using Common;
using Game;
using LogicInterfaces;

namespace ConsoleManagement
{
    public static class View
    {
        public static void SetObservedGameScreen(IGameScreen gameScreen)
        {
            Data.ObservedGameScreen = gameScreen;
        }

        public static void Refresh()
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

        public static void ShowStartView()
        {
            Console.WriteLine("Welcome to battleships!");
            Console.WriteLine(Data.MessageFirstLine);
            Console.WriteLine(Data.MessageSecondLine);
            Console.WriteLine("To start game type start");
        }

        public static void ShowOngoingView()
        {
            Console.WriteLine(Data.MessageFirstLine);
            Console.WriteLine(Data.MessageSecondLine);
            Console.SetCursorPosition(OwnBoardPositionX + 5, MessageSpaceY);
            Console.WriteLine("Your board");
            Console.SetCursorPosition(OpponentBoardPositionX + 3, MessageSpaceY);
            Console.WriteLine("Opponent board");

            PrepareBorders(OwnBoardPositionX);
            PrepareBorders(OpponentBoardPositionX);

            PrepareInterior(OwnBoardPositionX, Data.ObservedGameScreen.OwnBoard);
            PrepareInterior(OpponentBoardPositionX, Data.ObservedGameScreen.OpponentBoard);

            Console.SetCursorPosition(0, CommandPositionY);
        }

        public static void ShowEndView()
        {
            Console.WriteLine("Game has ended!");
            if (Data.Winner != null)
                Console.WriteLine(Data.Winner + " has won!");
            Console.WriteLine("Nobody has won!");
        }

        private static void PrepareInterior(int boardPosition, IGameboard board)
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

        private static void DrawField(Rules.FieldType[,] rawBoard, int x, int y)
        {
            switch (rawBoard[x, y])
            {
                case Rules.FieldType.Battleship:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write('#');
                    break;
                case Rules.FieldType.Empty:
                    Console.Write(' ');
                    break;
                case Rules.FieldType.Mishit:
                    Console.Write('-');
                    break;
                case Rules.FieldType.Hit:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write('H');
                    break;
                case Rules.FieldType.Sunken:
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
                Console.Write((char)('A' + j));
                Console.Write(" ");

                Console.SetCursorPosition(boardPositionX - 2, MessageSpaceY + 2 + j);
                Console.Write(j);
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        private const int OwnBoardPositionX = 3;
        private const int OpponentBoardPositionX = 30;
        private const int MessageSpaceY = 2;
        private const int CommandPositionY = MessageSpaceY + 2 + 10 + 1;
    }
}