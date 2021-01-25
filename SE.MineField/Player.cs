using System.Collections.Generic;
using System.Linq;
using SE.MineField.Models;

namespace SE.MineField
{
    public class Player : IPlayer
    {
        private int _xPosition;
        private int _yPosition;
        private int _remainingLives;
        private int _score;
        private IList<Move> _moves = new List<Move>();

        public Player()
        {
            _remainingLives = 5;
        }

        public int XPosition => _xPosition;
        public int YPosition => _yPosition;
        public bool IsAlive => _remainingLives > 0;

        public int RemainingLives => _remainingLives;
        public int Score => _score;
        public IList<Move> Moves => _moves;

        public void SetPosition(int xPosition, int yPosition)
        {
            _xPosition = xPosition;
            _yPosition = yPosition;

            if (_moves.Any())
            {
                _score++;
            }
            AddToMoves(xPosition, yPosition);
        }

        private void AddToMoves(int xPosition, int yPosition)
        {
            _moves.Add(new Move(xPosition, yPosition));
        }

        public void HitMine()
        {
            _remainingLives--;
        }
    }
}