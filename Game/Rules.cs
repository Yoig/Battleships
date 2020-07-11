using System;
using System.Collections.Generic;

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
            Sunken,
            Last
        }

        public enum Direction
        {
            Up,
            Down,
            Right,
            Left,
            Error
        }

        public static int BoardSize { get; } = 10;
        public static Dictionary<string, int> Battleships { get; set; } = new Dictionary<string, int>();
    }
}