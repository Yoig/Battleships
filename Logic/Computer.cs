using System;
using System.Threading;
using Common;
using Game;
using LogicInterfaces;

namespace Logic
{
    public class Computer : IPlayer
    {
        public IPlayer Opponent { get; private set; }
        public IGameScreen Screen { get; } = new GameScreen();

        public Rules.FieldState PlayTurn(string option)
        {
            Thread.Sleep(1000);
            Data.Message = "play turn computer";
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
                bool placedProperly;
                ICoordinate beginning;
                ICoordinate end = new Coordinate();
                do
                {
                    placedProperly = true;
                    beginning = Coordinate.Random();
                    var random = new Random();
                    try
                    {
                        end = beginning.Move(battleship.Value - 1, (Rules.Direction)random.Next(0, 4));
                    }
                    catch (ArgumentOutOfRangeException e)
                    {
                        placedProperly = false;
                    }
                } while (!placedProperly);
                Console.WriteLine("Random coordinates: beg = " + beginning + " end = " + end);
                Thread.Sleep(1000);
                Screen.PlaceBattleship(beginning, end);
            }
        }

        public void SetOpponent(IPlayer opponent)
        {
            Opponent = opponent;
        }
    }
}