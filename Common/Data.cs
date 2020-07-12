using System.Collections.Generic;
using LogicInterfaces;

namespace Common
{
    public static class Data
    {
        public enum GameState
        {
            NotStarted,
            Ongoing,
            Ended,
            Help
        }

        public static string MessageFirstLine { get; set; } = null;
        public static string MessageSecondLine { get; set; } = null;

        public struct PredefinedMessages
        {
            public const string WrongInput = "Wrong command! Please, type again";
        }
        public static GameState State { get; set; } = GameState.NotStarted;
        public static IPlayer Winner { get; set; } = null;
        public static Queue<IPlayer> Players { get; set; } = new Queue<IPlayer>();
        public static IGameScreen ObservedGameScreen { get; set; }
        public static bool Started { get; set; } = false;
    }
}
