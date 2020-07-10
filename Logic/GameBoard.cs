using System;
using Game;
using LogicInterfaces;

namespace Logic
{
    public class Gameboard : IGameboard
    {
        public Rules.FieldState[,] RawBoard { get; private set; } = new Rules.FieldState[Rules.BoardSize, Rules.BoardSize];

        public Gameboard()
        {
            //temporary for testing
            RawBoard[0, 0] = Rules.FieldState.Battleship;
            RawBoard[0, 1] = Rules.FieldState.Battleship;
            RawBoard[0, 2] = Rules.FieldState.Battleship;

            RawBoard[1, 0] = Rules.FieldState.Mishit;
            RawBoard[1, 1] = Rules.FieldState.Mishit;
            RawBoard[1, 2] = Rules.FieldState.Mishit;

            RawBoard[2, 0] = Rules.FieldState.Hit;
            RawBoard[2, 1] = Rules.FieldState.Hit;
            RawBoard[2, 2] = Rules.FieldState.Hit;

            RawBoard[3, 0] = Rules.FieldState.Sunken;
            RawBoard[3, 1] = Rules.FieldState.Sunken;
            RawBoard[3, 2] = Rules.FieldState.Sunken;
        }
    }
}