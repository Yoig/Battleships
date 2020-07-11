using System;
using Game;
using LogicInterfaces;
using Common;

namespace Logic
{
    public class Human : IPlayer
    {
        public IPlayer Opponent { get; private set; }

        public Rules.FieldState PlayTurn(string option)
        {
            Data.Message = "play turn human";
            return Rules.FieldState.Mishit;
        }

        public void Setup()
        {
            PlaceBattleships();
        }

        private void PlaceBattleships()
        {
            Data.Message = "placing battleships human";
        }

        public void SetOpponent(IPlayer opponent)
        {
            Opponent = opponent;
        }
    }
}