using Game;

namespace LogicInterfaces
{
    public interface IPlayer
    {
        IPlayer Opponent { get; }
        IGameScreen GameScreen { get; }
        Rules.FieldType PlayTurn(string option);
        void Setup();
        void SetOpponent(IPlayer opponent);
        Rules.FieldType ReceiveShot(ICoordinate coordinate);
    }
}