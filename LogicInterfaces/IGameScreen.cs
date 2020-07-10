namespace LogicInterfaces
{
    public interface IGameScreen
    {
        IGameboard GetOwnBoard();
        IGameboard GetOpponentBoard();
    }
}