﻿using System;
using Game;
using LogicInterfaces;

namespace Logic
{
    /// <summary>
    /// Representation of coordinates.
    /// </summary>
    public struct Coordinate : ICoordinate
    {
        /// <summary>
        /// Make coordinate from string in the form of letter + digit. e.g a7
        /// </summary>
        /// <param name="raw"></param>
        public Coordinate(string raw)
        {
            raw = raw.ToLower();
            _raw = raw;
            X = raw[0] - 'a';
            Y = raw[1] - '0';
        }

        public Coordinate(Coordinate coordinate)
        {
            this._raw = coordinate._raw;
            this.X = coordinate.X;
            this.Y = coordinate.Y;
        }

        /// <summary>
        /// Return direction of target relative to second coordinate.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="relativeTo"></param>
        /// <returns></returns>
        public static Rules.Direction DetermineDirection(ICoordinate target, ICoordinate relativeTo)
        {
            if (target.Y == relativeTo.Y)
                if (target.X > relativeTo.X)
                    return Rules.Direction.Right;
                else
                    return Rules.Direction.Left;
            if (target.X == relativeTo.X)
                if (target.Y > relativeTo.Y)
                    return Rules.Direction.Down;
                else
                    return Rules.Direction.Up;
            return Rules.Direction.All;
        }

        /// <summary>
        /// Returns randomly generated valid coordinate.
        /// </summary>
        /// <returns></returns>
        public static ICoordinate Random()
        {
            ICoordinate coordinate = new Coordinate("a0");
            var random = new Random();
            coordinate = coordinate.MoveDown(random.Next(0, 10));
            coordinate = coordinate.MoveRight(random.Next(0, 10));
            return coordinate;
        }

        /// <summary>
        /// Returns coordinate moved by amount in direction. If result coordinate went beyond board, throws exception.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public ICoordinate Move(int amount, Rules.Direction? direction)
        {
            switch (direction)
            {
                case Rules.Direction.Up:
                    return MoveUp(amount);
                case Rules.Direction.Right:
                    return MoveRight(amount);
                case Rules.Direction.Down:
                    return MoveDown(amount);
                case Rules.Direction.Left:
                    return MoveLeft(amount);
                default:
                    throw new ArgumentException();
            }
        }

        public ICoordinate MoveUp(int amount)
        {
            var coordinate = this;
            if ((coordinate.Y - amount) < 0)
                throw new ArgumentOutOfRangeException();
            coordinate.Y -= amount;
            coordinate._raw = coordinate._raw.Replace(coordinate._raw[1], (char) (coordinate._raw[1] - amount));
            return coordinate;
        }

        public ICoordinate MoveRight(int amount)
        {
            var coordinate = this;
            if ((coordinate.X + amount) >= Rules.BoardSize)
                throw new ArgumentOutOfRangeException();
            coordinate.X += amount;
            coordinate._raw = coordinate._raw.Replace(coordinate._raw[0], (char) (coordinate._raw[0] + amount));
            return coordinate;
        }

        public ICoordinate MoveDown(int amount)
        {
            var coordinate = this;
            if ((coordinate.Y + amount) >= Rules.BoardSize)
                throw new ArgumentOutOfRangeException();
            coordinate.Y += amount;
            coordinate._raw = coordinate._raw.Replace(coordinate._raw[1], (char) (coordinate._raw[1] + amount));
            return coordinate;
        }

        public ICoordinate MoveLeft(int amount)
        {
            var coordinate = this;
            if ((coordinate.X - amount) < 0)
                throw new ArgumentOutOfRangeException();
            coordinate.X -= amount;
            coordinate._raw = coordinate._raw.Replace(coordinate._raw[0], (char) (coordinate._raw[0] - amount));
            return coordinate;
        }

        public override string ToString()
        {
            return _raw;
        }

        /// <summary>
        /// Return distance between two coordinates.
        /// </summary>
        /// <param name="beginning"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static double Distance(ICoordinate beginning, ICoordinate end)
        {
            var XDifference = Math.Abs(beginning.X - end.X);
            var YDifference = Math.Abs(beginning.Y - end.Y);
            return Math.Sqrt(XDifference * XDifference + YDifference * YDifference);
        }

        /// <summary>
        /// Return coordinate moved to the right. If it would move coordinate beyond board, moves it at the beginning of the below line.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public static ICoordinate NextCoordinate(ICoordinate coordinate)
        {
            var next = (Coordinate)coordinate;
            try
            {
                next = (Coordinate)next.MoveRight(1);
            }
            catch (Exception)
            {
                next.X = 0;
                try
                {
                    next = (Coordinate)next.MoveDown(1);
                }
                catch (Exception)
                {
                    next.Y = 0;
                }
            }
            return next;
        }

        ICoordinate ICoordinate.NextCoordinate(ICoordinate coordinate)
        {
            return NextCoordinate(coordinate);
        }

        public int X { get; set; }
        public int Y { get; set; }

        private string _raw;

        /// <summary>
        /// Returns opposite direction of direction given.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Rules.Direction OppositeDirection(Rules.Direction? direction)
        {
            switch (direction)
            {
                case Rules.Direction.Up:
                    return Rules.Direction.Down;
                case Rules.Direction.Left:
                    return Rules.Direction.Right;
                case Rules.Direction.Right:
                    return Rules.Direction.Left;
                case Rules.Direction.Down:
                    return Rules.Direction.Up;
                default:
                    return Rules.Direction.All;
            }
        }

        /// <summary>
        /// Returns next direction clockwise.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Rules.Direction? NextDirection(Rules.Direction? direction)
        {
            switch (direction)
            {
                case Rules.Direction.Up:
                    return Rules.Direction.Right;
                case Rules.Direction.Right:
                    return Rules.Direction.Down;
                case Rules.Direction.Down:
                    return Rules.Direction.Left;
                case Rules.Direction.Left:
                    return Rules.Direction.Up;
                default:
                    return Rules.Direction.All;
            }
        }
    }
}