namespace LogicInterfaces
{
    public interface IGameScreen
    {
        IGameboard OwnBoard { get; set; }
        IGameboard OpponentBoard { get; set; }
        void PlaceBattleship(ICoordinate beginning, ICoordinate end);
    }
}