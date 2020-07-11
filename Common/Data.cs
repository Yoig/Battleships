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
            Ended
        }

        public static string Message { get; set; } = null;

        public struct PredefinedMessages
        {
            public const string WrongInput = "Wrong command! Please, type again";
        }
        public static GameState State { get; set; } = GameState.NotStarted;
        public static IPlayer Winner { get; set; }
        public static Queue<IPlayer> Players { get; set; } = new Queue<IPlayer>();
    }
}
