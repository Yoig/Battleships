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

        public Rules.FieldState PlayTurn(string option)
        {
            return Rules.FieldState.Mishit;
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
    }
}