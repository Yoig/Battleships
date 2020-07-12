using System.Collections.Generic;

namespace Game
{
    /// <summary>
    /// Parameters of game.
    /// </summary>
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
            All
        }

        public static int BoardSize { get; } = 10;
        public static Dictionary<string, int> Battleships { get; set; } = new Dictionary<string, int>();
    }
}