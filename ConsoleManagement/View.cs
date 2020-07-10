using System;
using System.Buffers;
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

        public void StartMessage()
        {
            Console.Clear();
            Console.WriteLine("Welcome to battleships!");
            Console.Write("To start game type ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("start");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Update()
        {
            Console.Clear();
            Console.SetCursorPosition(OwnBoardPosition + 5, MessageSpace);
            Console.WriteLine("Your board");
            Console.SetCursorPosition(OpponentBoardPosition + 3, MessageSpace);
            Console.WriteLine("Opponent board");

            PrepareBorders(OwnBoardPosition);
            PrepareBorders(OpponentBoardPosition);

            PrepareInterior(OwnBoardPosition, _observedGameScreen.GetOwnBoard());
            PrepareInterior(OpponentBoardPosition, _observedGameScreen.GetOpponentBoard());
        }

        private static void PrepareInterior(int boardPosition, IGameboard board)
        {
            var rawBoard = board.RawBoard;
            for (var y = 0; y < 10; y++)
            {
                Console.SetCursorPosition(boardPosition, MessageSpace + 2 + y);
                for (var x = 0; x < 10; x++)
                {
                    DrawField(rawBoard, x, y);
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

        private static void PrepareBorders(int boardPosition)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            for (var j = 0; j < 10; j += 1)
            {
                Console.SetCursorPosition(boardPosition + 2 * j, MessageSpace + 1);
                Console.Write((char) ('A' + j));
                Console.Write(" ");

                Console.SetCursorPosition(boardPosition - 2, MessageSpace + 2 + j);
                Console.Write(j);
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        private const int OwnBoardPosition = 3;
        private const int OpponentBoardPosition = 30;
        private const int MessageSpace = 2;

        private IGameScreen _observedGameScreen = null;
    }
}