using Game;

namespace LogicInterfaces
{
    public interface IGameboard
    {
        Rules.FieldState[,] RawBoard { get; }
        void PlaceBattleship(ICoordinate beginning, ICoordinate end);
    }
}