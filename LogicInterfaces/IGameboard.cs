using Game;

namespace LogicInterfaces
{
    public interface IGameboard
    {
        Rules.FieldState[,] RawBoard { get; set; }
        void PlaceBattleship(ICoordinate beginning, ICoordinate end);
    }
}