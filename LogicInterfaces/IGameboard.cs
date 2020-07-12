using Game;

namespace LogicInterfaces
{
    public interface IGameboard
    {
        int BattleshipsRemaining { get; set; }
        Rules.FieldType[,] RawBoard { get; }
        void PlaceBattleship(ICoordinate beginning, ICoordinate end);
        void MarkField(ICoordinate coordinate, Rules.FieldType shotResult);
        Rules.FieldType ReceiveShoot(ICoordinate coordinate);
    }
}