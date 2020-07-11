using Game;

namespace LogicInterfaces
{
    public interface IGameScreen
    {
        IGameboard OwnBoard { get; }
        IGameboard OpponentBoard { get; }
        void PlaceBattleship(ICoordinate beginning, ICoordinate end);
        void MarkField(ICoordinate coordinate, Rules.FieldType shotResult);
        Rules.FieldType receiveShot(ICoordinate coordinate);
    }
}