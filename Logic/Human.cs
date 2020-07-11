using System;
using Game;
using LogicInterfaces;
using Common;
using ConsoleManagement;

namespace Logic
{
    public class Human : IPlayer
    {
        public Human(IGameScreen screen)
        {
            Screen = screen;
        }

        public IPlayer Opponent { get; private set; }
        public IGameScreen Screen { get; }

        public Rules.FieldType PlayTurn(string option)
        {
            var coordinate = new Coordinate(option);
            var shotResult = Opponent.ReceiveShot(coordinate);
            switch (shotResult)
            {
                case Rules.FieldType.Mishit:
                    Data.MessageFirstLine = "Mishit!";
                    break;
                case Rules.FieldType.Hit:
                    Data.MessageFirstLine = "Opponent's battleship hit!";
                    break;
                case Rules.FieldType.Sunken:
                    Data.MessageFirstLine = "Opponent's battleship destroyed!";
                    break;
                case Rules.FieldType.Last:
                    Data.MessageFirstLine = "That was last battleship!";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return shotResult;
        }

        public void Setup()
        {
            PlaceBattleships();
        }

        private void PlaceBattleships()
        {
            foreach (var battleship in Rules.Battleships)
            {
                bool coordinatesValid;
                do
                {
                    coordinatesValid = true;
                    Data.MessageFirstLine = "Place " + battleship.Key + " of length " + battleship.Value;
                    View.Refresh();
                    var beginning = ReadValidCoordinate("First coordinate");
                    var end = ReadValidCoordinate("Second coordinate");
                    try
                    {
                        beginning = beginning.Move(0, Rules.Direction.Up);
                        end = end.Move(0, Rules.Direction.Up);
                        if (Coordinate.Distance(beginning, end) != battleship.Value - 1)
                            throw new ArgumentException();
                        Screen.PlaceBattleship(beginning, end);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        Data.MessageSecondLine = "Wrong coordinate!";
                        coordinatesValid = false;
                    }
                    catch (ArgumentException e)
                    {
                        Data.MessageSecondLine = "Wrong ship position!";
                        coordinatesValid = false;
                    }
                } while (!coordinatesValid);
                Data.MessageSecondLine = "";
            }
        }

        private ICoordinate ReadValidCoordinate(string prompt = null)
        {
            Coordinate coordinate = new Coordinate("a0");
            bool coordinatesValid;
            do
            {
                coordinatesValid = true;
                try
                {
                    var input = Input.ReadCoordinate(prompt);
                    coordinate = new Coordinate(input);
                }
                catch (ArgumentException e)
                {
                    coordinatesValid = false;
                }
            } while (!coordinatesValid);

            return coordinate;
        }

        public void SetOpponent(IPlayer opponent)
        {
            Opponent = opponent;
        }

        public Rules.FieldType ReceiveShot(ICoordinate coordinate)
        {
            return Screen.receiveShot(coordinate);
        }
    }
}