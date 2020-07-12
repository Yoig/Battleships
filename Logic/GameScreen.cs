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
            OpponentBoard.MarkField(coordinate, shotResult);
        }

        public Rules.FieldType receiveShot(ICoordinate coordinate)
        {
            return OwnBoard.ReceiveShoot(coordinate);
        }

        public void SetBattleshipsRemaining(int battleshipsCount)
        {
            OwnBoard.BattleshipsRemaining = battleshipsCount;
        }
    }
}