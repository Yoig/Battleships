using Game;

namespace LogicInterfaces
{
    public interface IGameboard
    {
        Rules.FieldType[,] RawBoard { get; }
        void PlaceBattleship(ICoordinate beginning, ICoordinate end);
        void MarkField(ICoordinate coordinate, Rules.FieldType shotResult);
        Rules.FieldType ReceiveShoot(ICoordinate coordinate);
    }
}