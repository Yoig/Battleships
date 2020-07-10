using Game;

namespace LogicInterfaces
{
    public interface IGameboard
    {
        Rules.FieldState[,] RawBoard { get; }
    }
}