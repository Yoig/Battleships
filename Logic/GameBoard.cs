using System;
using Game;
using LogicInterfaces;

namespace Logic
{
    public class Gameboard : IGameboard
    {
        public int BattleshipsRemaining { get; set; }
        public Rules.FieldType[,] RawBoard { get; set; } = new Rules.FieldType[Rules.BoardSize, Rules.BoardSize];

        public void PlaceBattleship(ICoordinate beginning, ICoordinate end)
        {
            var direction = Coordinate.DetermineDirection(end, beginning);
            if (IsLineAdjacentToFieldType(beginning, end, Rules.FieldType.Battleship))
                throw new ArgumentException();
            var field = beginning;
            PlaceFields(end, field, direction);
        }

        public void MarkField(ICoordinate coordinate, Rules.FieldType shotResult)
        {
            RawBoard[coordinate.X, coordinate.Y] = shotResult;
            if (shotResult == Rules.FieldType.Sunken)
                SunkBattleship(coordinate);
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
            else if (RawBoard[coordinate.X, coordinate.Y] == Rules.FieldType.Empty)
                RawBoard[coordinate.X, coordinate.Y] = battleshipCondition = Rules.FieldType.Mishit;
            else
                battleshipCondition = RawBoard[coordinate.X, coordinate.Y];

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

            if (IsFieldAdjacentToFieldType(field, Rules.FieldType.Hit, Rules.Direction.Down))
                SunkField(GetAdjacentFieldType(field, Rules.FieldType.Hit, Rules.Direction.Down));
            if (IsFieldAdjacentToFieldType(field, Rules.FieldType.Hit, Rules.Direction.Up))
                SunkField(GetAdjacentFieldType(field, Rules.FieldType.Hit, Rules.Direction.Up));
            if (IsFieldAdjacentToFieldType(field, Rules.FieldType.Hit, Rules.Direction.Right))
                SunkField(GetAdjacentFieldType(field, Rules.FieldType.Hit, Rules.Direction.Right));
            if (IsFieldAdjacentToFieldType(field, Rules.FieldType.Hit, Rules.Direction.Left))
                SunkField(GetAdjacentFieldType(field, Rules.FieldType.Hit, Rules.Direction.Left));
        }

        private ICoordinate GetAdjacentFieldType(ICoordinate field, Rules.FieldType fieldType, Rules.Direction direction)
        {
            try
            {
                var upper = field.Move(1, direction);
                if (RawBoard[upper.X, upper.Y] == fieldType)
                    return upper;
            }
            catch (Exception)
            {
                // ignored
            }
            throw new MissingFieldException();
        }

        private Rules.FieldType CheckCondition(ICoordinate coordinate)
        {
            if (CheckLineCondition(coordinate, Rules.Direction.Up) == Rules.FieldType.Hit)
                return Rules.FieldType.Hit;
            if (CheckLineCondition(coordinate, Rules.Direction.Down) == Rules.FieldType.Hit)
                return Rules.FieldType.Hit;
            if (CheckLineCondition(coordinate, Rules.Direction.Right) == Rules.FieldType.Hit)
                return Rules.FieldType.Hit;
            if (CheckLineCondition(coordinate, Rules.Direction.Left) == Rules.FieldType.Hit)
                return Rules.FieldType.Hit;
            return Rules.FieldType.Sunken;
        }

        private Rules.FieldType CheckLineCondition(ICoordinate coordinate, Rules.Direction direction)
        {
            ICoordinate movedTo;
            try
            {
                movedTo = coordinate.Move(1, direction);
            }
            catch
            {
                return Rules.FieldType.Sunken;
            }

            if (RawBoard[movedTo.X, movedTo.Y] == Rules.FieldType.Empty)
                return Rules.FieldType.Sunken;
            if (RawBoard[movedTo.X, movedTo.Y] == Rules.FieldType.Battleship)
                return Rules.FieldType.Hit;

            return CheckLineCondition(movedTo, direction);
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

        public bool IsLineAdjacentToFieldType(ICoordinate beginning, ICoordinate end, Rules.FieldType fieldType)
        {
            var direction = Coordinate.DetermineDirection(end, beginning);
            var field = beginning;

            do
            {
                if (IsFieldAdjacentToFieldType(field, fieldType)) return true;

                if ((field.X != end.X) || (field.Y != end.Y))
                    field = field.Move(1, direction);
            } while ((field.X != end.X) || (field.Y != end.Y));
            //check last field
            if (IsFieldAdjacentToFieldType(field, fieldType)) return true;

            return false;
        }

        private bool IsFieldAdjacentToFieldType(ICoordinate field, Rules.FieldType fieldType, Rules.Direction direction)
        {
            try
            {
                var upper = field.Move(1, direction);
                if (RawBoard[upper.X, upper.Y] == fieldType)
                    return true;
            }
            catch (Exception)
            {
                // ignored
            }

            return false;
        }

        private bool IsFieldAdjacentToFieldType(ICoordinate field, Rules.FieldType fieldType)
        {
            if (IsFieldAdjacentToFieldType(field, fieldType, Rules.Direction.Up))
                return true;
            if (IsFieldAdjacentToFieldType(field, fieldType, Rules.Direction.Down))
                return true;
            if (IsFieldAdjacentToFieldType(field, fieldType, Rules.Direction.Right))
                return true;
            if (IsFieldAdjacentToFieldType(field, fieldType, Rules.Direction.Left))
                return true;

            return false;
        }
    }
}