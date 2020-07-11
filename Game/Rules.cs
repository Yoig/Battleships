using System;
using System.Collections.Generic;

namespace Game
{
    public struct Rules
    {
        public enum FieldType
        {
            Empty,
            Battleship,
            Mishit,
            Hit,
            Sunken,
            Last,
            Error
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