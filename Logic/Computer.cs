using System;
using System.Threading;
using Common;
using Game;
using LogicInterfaces;

namespace Logic
{
    public class Computer : IPlayer
    {
        public Computer(IGameScreen screen)
        {
            Screen = screen;
        }

        public IPlayer Opponent { get; private set; }
        public IGameScreen Screen { get; }

        public Rules.FieldType PlayTurn(string option)
        {
            Thread.Sleep(1000);
            throw new NotImplementedException();
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
                ICoordinate beginning;
                ICoordinate end = new Coordinate();
                do
                {
                    coordinatesValid = true;
                    beginning = Coordinate.Random();
                    var random = new Random();
                    try
                    {
                        end = beginning.Move(battleship.Value - 1, (Rules.Direction) random.Next(0, 4));
                        Screen.PlaceBattleship(beginning, end);
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        coordinatesValid = false;
                    }
                    catch (ArgumentException e)
                    {
                        coordinatesValid = false;
                    }
                } while (!coordinatesValid);
            }
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