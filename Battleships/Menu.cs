using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;
using ConsoleManagement;
using Game;
using LogicInterfaces;

namespace Battleships
{
    internal static class Menu
    {
        public static void Option(string option)
        {
            if(option.ToLower() == "start")
                Start();
            if(option.ToLower() == "exit")
                Exit();
        }

        public static void Start()
        {
            Data.State = Data.GameState.Ongoing;
            SetupPlayers();
        }

        private static void SetupPlayers()
        {
            foreach (var player in Data.Players)
            {
                player.Setup();
            }
            Data.MessageFirstLine = "Setup complete! To play, type coordinates to shoot at";
            View.Refresh();
        }

        public static void Exit()
        {
            Data.State = Data.GameState.Ended;
        }

        public static void ChangeScreen(IGameScreen gameScreen)
        {
            Data.ObservedGameScreen = gameScreen;
            View.Refresh();
        }
    }
}
