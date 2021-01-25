using System.Collections.Generic;

namespace SE.MineField.Models
{
    public interface IPlayer
    {
        int XPosition { get; }
        int YPosition { get; }
        bool IsAlive { get; }
        int RemainingLives { get; }
        int Score { get; }
        IList<Move> Moves { get; }
        void SetPosition(int xPosition, int yPosition);
        void HitMine();
    }
}