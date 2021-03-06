﻿using System;
using System.Threading;
using Common;
using ConsoleManagement;
using Game;
using LogicInterfaces;

namespace Logic
{
    /// <summary>
    /// AI player.
    /// </summary>
    public class Computer : IPlayer
    {
        private Rules.FieldType _previousShotOutcome = Rules.FieldType.Mishit;
        private ICoordinate _previousShotCoordinate;
        private ICoordinate _currentNotSunkenFirstHitCoordinate = null;
        private Rules.Direction? _shootingDirection = null;
        private Rules.Direction? _guessShootingDirection = null;

        public Computer(IGameScreen screen)
        {
            GameScreen = screen;
        }

        public IPlayer Opponent { get; private set; }
        public IGameScreen GameScreen { get; }

        /// <summary>
        /// Manages proper turn actions like shooting.
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public Rules.FieldType PlayTurn(string option)
        {
            Data.MessageSecondLine = "Computer is making choice...";
            View.Refresh();
            Thread.Sleep(2000);
            ICoordinate coordinate = MakeShot();
            _previousShotCoordinate = coordinate;
            var shotResult = Opponent.ReceiveShot(coordinate);
            GameScreen.MarkField(coordinate, shotResult);
            _previousShotOutcome = shotResult;
            return shotResult;
        }

        /// <summary>
        /// Generates coordinates in smart way. When battleship is hit, next coordinate will be generated to sunk that ship.
        /// </summary>
        /// <returns></returns>
        private ICoordinate MakeShot()
        {
            var random = new Random();
            ICoordinate coordinate = new Coordinate();
            switch (_previousShotOutcome)
            {
                case Rules.FieldType.Sunken:
                case Rules.FieldType.Mishit:
                {
                    if (_previousShotOutcome == Rules.FieldType.Sunken)
                    {
                        _currentNotSunkenFirstHitCoordinate = null;
                        _shootingDirection = null;
                    }

                    if (_guessShootingDirection != null)
                    {
                        var direction = TryGuessDirection(_previousShotCoordinate);
                        coordinate = _currentNotSunkenFirstHitCoordinate.Move(1, direction);
                    }
                    else
                    {
                        coordinate = Coordinate.Random();
                        while (!GameScreen.IsOpponentFieldEmpty(coordinate))
                            coordinate = coordinate.NextCoordinate(coordinate);
                    }
                }
                    break;
                case Rules.FieldType.Hit:
                {
                    if (_currentNotSunkenFirstHitCoordinate == null)
                        _currentNotSunkenFirstHitCoordinate = _previousShotCoordinate;
                    if (_guessShootingDirection != null)
                    {
                        _shootingDirection = _guessShootingDirection;
                        _guessShootingDirection = null;
                    }

                    if (_shootingDirection != null)
                    {
                        try
                        {
                            coordinate = _previousShotCoordinate.Move(1, _shootingDirection);
                        }
                        catch (Exception)
                        {
                            _shootingDirection = Coordinate.OppositeDirection(_shootingDirection);
                            coordinate = _currentNotSunkenFirstHitCoordinate.Move(1, _shootingDirection);
                        }
                    }
                    else
                    {
                        var direction = TryGuessDirection(_previousShotCoordinate);
                        coordinate = _previousShotCoordinate.Move(1, direction);
                    }
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return coordinate;
        }

        /// <summary>
        /// Sets next direction clockwise. If board end is in specified direction, it skips to next direction.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        private Rules.Direction? TryGuessDirection(ICoordinate coordinate)
        {
            bool directionIsGood = false;
            while (!directionIsGood)
            {
                try
                {
                    _guessShootingDirection = _guessShootingDirection == null
                        ? Rules.Direction.Up
                        : Coordinate.NextDirection(_guessShootingDirection);
                    coordinate.Move(1, _guessShootingDirection);
                    directionIsGood = true;
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return _guessShootingDirection;
        }

        /// <summary>
        /// Prepares board to play.
        /// </summary>
        public void Setup()
        {
            PlaceBattleships();
            GameScreen.SetBattleshipsRemaining(Rules.Battleships.Count);
        }

        /// <summary>
        /// Places battleships on board in random way, but battleships cannot touch each other with sides.
        /// </summary>
        private void PlaceBattleships()
        {
            foreach (var battleship in Rules.Battleships)
            {
                bool coordinatesValid;
                ICoordinate beginning;
                ICoordinate end = new Coordinate();
                do
                {
                    coordinatesValid = true;
                    beginning = Coordinate.Random();
                    var random = new Random();
                    try
                    {
                        end = beginning.Move(battleship.Value - 1, (Rules.Direction) random.Next(0, 4));
                        GameScreen.PlaceBattleship(beginning, end);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        coordinatesValid = false;
                    }
                    catch (ArgumentException)
                    {
                        coordinatesValid = false;
                    }
                } while (!coordinatesValid);
            }
        }

        public void SetOpponent(IPlayer opponent)
        {
            Opponent = opponent;
        }

        /// <summary>
        /// Marks shot on own gameboard, and checks shot result (like sunken, mishit). 
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Rules.FieldType ReceiveShot(ICoordinate coordinate)
        {
            return GameScreen.receiveShot(coordinate);
        }

        public override string ToString()
        {
            return "Computer Player";
        }
    }
}