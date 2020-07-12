using System;
using System.ComponentModel;
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
            GameScreen = screen;
        }

        public IPlayer Opponent { get; private set; }
        public IGameScreen GameScreen { get; }

        public Rules.FieldType PlayTurn(string option)
        {
            Thread.Sleep(2000);
            ICoordinate coordinate = MakeShot();
            var shotResult = Opponent.ReceiveShot(coordinate);
            GameScreen.MarkField(coordinate, shotResult);
            return shotResult;
        }

        private ICoordinate MakeShot()
        {
            var random = new Random();
            return new Coordinate("a" + random.Next(0,10));
        }

        public void Setup()
        {
            PlaceBattleships();
            GameScreen.SetBattleshipsRemaining(Rules.Battleships.Count);
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
                        GameScreen.PlaceBattleship(beginning, end);
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
            return GameScreen.receiveShot(coordinate);
        }

        public override string ToString()
        {
            return "Computer";
        }
    }
}