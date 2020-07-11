using System;
using System.Net.Http;

namespace Game
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
        public static string Winner { get; set; } = "";
    }
}
