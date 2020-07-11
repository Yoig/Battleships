using Game;
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

        public void MarkField(ICoordinate coordinate, Rules.FieldType shotResult)
        {
            throw new System.NotImplementedException();
        }

        public Rules.FieldType receiveShot(ICoordinate coordinate)
        {
            throw new System.NotImplementedException();
        }
    }
}