using LogicInterfaces;

namespace Logic
{
    public class GameScreen : IGameScreen
    {
        public IGameboard OwnBoard { get; } = new Gameboard();
        public IGameboard OpponentBoard { get; } = new Gameboard();

        public void PlaceBattleship(ICoordinate beginning, ICoordinate end)
        {
            OwnBoard.PlaceBattleship(beginning, end);
        }
    }
}