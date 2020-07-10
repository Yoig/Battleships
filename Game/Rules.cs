using System;

namespace Game
{
    public struct Rules
    {
        public enum FieldState
        {
            Empty,
            Battleship,
            Mishit,
            Hit,
            Sunken
        }

        public static int BoardSize { get; } = 10;
    }
}