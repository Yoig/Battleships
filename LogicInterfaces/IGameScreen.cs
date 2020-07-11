namespace LogicInterfaces
{
    public interface IGameScreen
    {
        IGameboard OwnBoard { get; }
        IGameboard OpponentBoard { get; }
        void PlaceBattleship(ICoordinate beginning, ICoordinate end);
    }
}