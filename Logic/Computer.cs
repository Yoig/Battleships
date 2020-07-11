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
            Data.Message = "placing battleships computer";
        }

        public void SetOpponent(IPlayer opponent)
        {
            Opponent = opponent;
        }
    }
}