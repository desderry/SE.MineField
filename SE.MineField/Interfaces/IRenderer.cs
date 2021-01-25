using SE.MineField.Models;

namespace SE.MineField.Interfaces
{
    public interface IRenderer
    {
        void DrawBoard(IGameBoard board, IPlayer player);

        void DrawLives(IPlayer player);

        void DrawScore(IPlayer player);

        void DrawWinner(IPlayer player);
    }
}