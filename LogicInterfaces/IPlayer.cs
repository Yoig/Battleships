using System;
using Game;

namespace LogicInterfaces
{
    public interface IPlayer
    {
        IPlayer Opponent { get; }
        IGameScreen Screen { get; }
        Rules.FieldState PlayTurn(string option);
        void Setup();
        void SetOpponent(IPlayer opponent);
    }
}