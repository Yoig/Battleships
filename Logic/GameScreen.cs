using LogicInterfaces;

namespace Logic
{
    public class GameScreen : IGameScreen
    {


        private IGameboard _ownBoard = new Gameboard();
        private IGameboard _opponentBoard = new Gameboard();

        public IGameboard GetOwnBoard()
        {
            return _ownBoard;
        }

        public IGameboard GetOpponentBoard()
        {
            return _opponentBoard;
        }
    }
}