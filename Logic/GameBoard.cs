using System;
using Game;
using LogicInterfaces;

namespace Logic
{
    public class Gameboard : IGameboard
    {
        public int BattleshipsRemaining { get; private set; } = 0;
        public Rules.FieldType[,] RawBoard { get; set; } = new Rules.FieldType[Rules.BoardSize, Rules.BoardSize];

        public void PlaceBattleship(ICoordinate beginning, ICoordinate end)
        {
            var direction = Coordinate.DetermineDirection(end, beginning);
            if (IsPlaceAdjacentToFieldType(beginning, end, Rules.FieldType.Battleship))
                throw new ArgumentException();
            var field = beginning;
            PlaceFields(end, field, direction);
        }

        public void MarkField(ICoordinate coordinate, Rules.FieldType shotResult)
        {
            throw new NotImplementedException();
        }

        public Rules.FieldType ReceiveShoot(ICoordinate coordinate)
        {
            Rules.FieldType battleshipCondition;
            if (RawBoard[coordinate.X, coordinate.Y] == Rules.FieldType.Battleship)
            {
                battleshipCondition = CheckCondition(coordinate);
                if (battleshipCondition == Rules.FieldType.Sunken)
                {
                    bool wasLastBattleship = SunkBattleship(coordinate);
                    if (wasLastBattleship)
                        battleshipCondition = Rules.FieldType.Last;
                }
                else
                    RawBoard[coordinate.X, coordinate.Y] = battleshipCondition = Rules.FieldType.Hit;
            }
            else
                RawBoard[coordinate.X, coordinate.Y] = battleshipCondition = Rules.FieldType.Mishit;

            return battleshipCondition;
        }

        private bool SunkBattleship(ICoordinate field)
        {
            SunkField(field);
            BattleshipsRemaining--;
            bool wasLastBattleship = BattleshipsRemaining <= 0;
            return wasLastBattleship;
        }

        private void SunkField(ICoordinate field)
        {
            RawBoard[field.X, field.Y] = Rules.FieldType.Sunken;

            if (IsPlaceAdjacentToFieldType(field, field, Rules.FieldType.Hit))
                return;

            SunkField(GetAdjacentFieldType(field, Rules.FieldType.Hit));
        }

        private ICoordinate GetAdjacentFieldType(ICoordinate field, Rules.FieldType fieldType)
        {
            try
            {
                var upper = field.MoveUp(1);
                if (RawBoard[upper.X, upper.Y] == fieldType)
                    return upper;
            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                var left = field.MoveLeft(1);
                if (RawBoard[left.X, left.Y] == fieldType)
                    return left;
            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                var lower = field.MoveDown(1);
                if (RawBoard[lower.X, lower.Y] == fieldType)
                    return lower;
            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                var right = field.MoveRight(1);
                if (RawBoard[right.X, right.Y] == fieldType)
                    return right;
            }
            catch (Exception e)
            {
                // ignored
            }

            throw new MissingFieldException();
        }

        private Rules.FieldType CheckCondition(ICoordinate coordinate)
        {
            if (IsPlaceAdjacentToFieldType(coordinate, coordinate, Rules.FieldType.Battleship))
                return Rules.FieldType.Hit;
            else
                return Rules.FieldType.Sunken;
        }

        private void PlaceFields(ICoordinate end, ICoordinate field, Rules.Direction direction)
        {
            do
            {
                RawBoard[field.X, field.Y] = Rules.FieldType.Battleship;
                if ((field.X != end.X) || (field.Y != end.Y))
                    field = field.Move(1, direction);
            } while ((field.X != end.X) || (field.Y != end.Y));

            //place last field
            RawBoard[field.X, field.Y] = Rules.FieldType.Battleship;
        }

        public bool IsPlaceAdjacentToFieldType(ICoordinate beginning, ICoordinate end, Rules.FieldType fieldType)
        {
            var direction = Coordinate.DetermineDirection(end, beginning);
            var field = beginning;
            do
            {
                try
                {
                    var upper = field.MoveUp(1);
                    if (RawBoard[upper.X, upper.Y] == fieldType)
                        return true;
                }
                catch (Exception e)
                {
                    // ignored
                }

                try
                {
                    var left = field.MoveLeft(1);
                    if (RawBoard[left.X, left.Y] == fieldType)
                        return true;
                }
                catch (Exception e)
                {
                    // ignored
                }

                try
                {
                    var lower = field.MoveDown(1);
                    if (RawBoard[lower.X, lower.Y] == fieldType)
                        return true;
                }
                catch (Exception e)
                {
                    // ignored
                }

                try
                {
                    var right = field.MoveRight(1);
                    if (RawBoard[right.X, right.Y] == fieldType)
                        return true;
                }
                catch (Exception e)
                {
                    // ignored
                }

                if ((field.X != end.X) || (field.Y != end.Y))
                    field = field.Move(1, direction);
            } while ((field.X != end.X) || (field.Y != end.Y));

            return false;
        }
    }
}