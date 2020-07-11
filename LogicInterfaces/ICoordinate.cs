using System;
using Game;

namespace LogicInterfaces
{
    public interface ICoordinate
    {
        ICoordinate Move(int amount, Rules.Direction direction);
        ICoordinate MoveUp(int amount);
        ICoordinate MoveRight(int amount);
        ICoordinate MoveDown(int amount);
        ICoordinate MoveLeft(int amount);
        int X { get; }
        int Y { get; }
    }
}