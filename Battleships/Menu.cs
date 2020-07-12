using System.ComponentModel;
using Common;
using ConsoleManagement;
using LogicInterfaces;

namespace Battleships
{
    /// <summary>
    /// Manages options specified by user.
    /// </summary>
    internal static class Menu
    {
        /// <summary>
        /// Invokes specific private functions depending on the option.
        /// Possible options are: start, exit, help, back.
        /// </summary>
        /// <param name="option"></param>
        public static void Option(string option)
        {
            if(option.ToLower() == "start")
                Start();
            if(option.ToLower() == "exit")
                Exit();
            if(option.ToLower() == "help")
                Help();
            if (option.ToLower() == "back")
                Back();
        }

        private static void Back()
        {
            if (Data.State == Data.GameState.Help)
                Data.State = Data.GameState.Ongoing;
        }

        public static void Start()
        {
            if (Data.Started == false)
            {
                Data.State = Data.GameState.Ongoing; 
                Data.Started = true;
                SetupPlayers();
            }
        }

        public static void Help()
        {
            Data.State = Data.GameState.Help;
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
    }
}
