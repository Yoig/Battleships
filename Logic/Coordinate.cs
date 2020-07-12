using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using Game;
using LogicInterfaces;

namespace Logic
{
    public struct Coordinate : ICoordinate
    {
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

        public static ICoordinate Random()
        {
            ICoordinate coordinate = new Coordinate("a0");
            var random = new Random();
            coordinate = coordinate.MoveDown(random.Next(0, 10));
            coordinate = coordinate.MoveRight(random.Next(0, 10));
            return coordinate;
        }

        public ICoordinate Move(int amount, Rules.Direction direction)
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

        public int X { get; private set; }
        public int Y { get; private set; }

        private string _raw;

        public static double Distance(ICoordinate beginning, ICoordinate end)
        {
            var XDifference = Math.Abs(beginning.X - end.X);
            var YDifference = Math.Abs(beginning.Y - end.Y);
            return Math.Sqrt(XDifference * XDifference + YDifference * YDifference);
        }
    }
}