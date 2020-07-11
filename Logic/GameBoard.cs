using System;
using Game;
using LogicInterfaces;

namespace Logic
{
    public class Gameboard : IGameboard
    {
        public Rules.FieldState[,] RawBoard { get; set; } = new Rules.FieldState[Rules.BoardSize, Rules.BoardSize];
        public void PlaceBattleship(ICoordinate beginning, ICoordinate end)
        {
            var direction = Coordinate.DetermineDirection(end, beginning);
            if (PlaceIsValid(beginning, end, direction) == false)
                throw new ArgumentException();
            var field = beginning;
            PlaceFields(end, field, direction);
        }

        private void PlaceFields(ICoordinate end, ICoordinate field, Rules.Direction direction)
        {
            do
            {
                RawBoard[field.X, field.Y] = Rules.FieldState.Battleship;
                if((field.X != end.X) || (field.Y != end.Y))
                    field = field.Move(1, direction);
            } while ((field.X != end.X) || (field.Y != end.Y));

            //place last field
            RawBoard[field.X, field.Y] = Rules.FieldState.Battleship;
        }

        public bool PlaceIsValid(ICoordinate beginning, ICoordinate end, Rules.Direction direction)
        {
            var field = beginning;
            do
            {
                try
                {
                    var upper = field.MoveUp(1);
                    if (RawBoard[upper.X, upper.Y] == Rules.FieldState.Battleship)
                        return false;
                }
                catch (Exception e)
                {
                    // ignored
                }

                try
                {
                    var left = field.MoveLeft(1);
                    if (RawBoard[left.X, left.Y] == Rules.FieldState.Battleship)
                        return false;
                }
                catch (Exception e)
                {
                    // ignored
                }

                try
                {
                    var lower = field.MoveDown(1);
                    if (RawBoard[lower.X, lower.Y] == Rules.FieldState.Battleship)
                        return false;
                }
                catch (Exception e)
                {
                    // ignored
                }

                try
                {
                    var right = field.MoveRight(1);
                    if (RawBoard[right.X, right.Y] == Rules.FieldState.Battleship)
                        return false;
                }
                catch (Exception e)
                {
                    // ignored
                }

                if((field.X != end.X) || (field.Y != end.Y))
                    field = field.Move(1, direction);
            } while ((field.X != end.X) || (field.Y != end.Y));
            return true;
        }
    }
}