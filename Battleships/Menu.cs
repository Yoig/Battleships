using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        }

        private void Exit()
        {
            Data.State = Data.GameState.Ended;
        }
    }
}
