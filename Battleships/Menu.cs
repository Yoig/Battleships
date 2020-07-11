using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;
using Game;

namespace Battleships
{
    internal class Menu
    {
        public void Option(string option)
        {
            if(option.ToLower() == "start")
                Start();
            if(option.ToLower() == "exit")
                Exit();
        }

        private void Start()
        {
            Data.State = Data.GameState.Ongoing;
            SetupPlayers();
        }

        private void SetupPlayers()
        {
            foreach (var player in Data.Players)
            {
                player.Setup();
            }
        }

        private void Exit()
        {
            Data.State = Data.GameState.Ended;
        }
    }
}
