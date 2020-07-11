using LogicInterfaces;

namespace Logic
{
    public class GameScreen : IGameScreen
    {
        public IGameboard OwnBoard { get; set; } = new Gameboard();
        public IGameboard OpponentBoard { get; set; } = new Gameboard();

        public void PlaceBattleship(ICoordinate beginning, ICoordinate end)
        {
            OwnBoard.PlaceBattleship(beginning, end);
        }
    }
}