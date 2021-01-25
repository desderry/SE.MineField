using System.Collections.Generic;
using SE.MineField.Enums;

namespace SE.MineField.Models
{
    public interface IGameBoard
    {
        SquareType[,] Board { get; set; }
        int Size { get; set; }
        Dictionary<int, string> YLabels { get; set; }
        Dictionary<int, string> XLabels { get; set; }

        public GameBoard Generate(int size);

        bool IsValidSquare(int playerXPosition, int playerYPosition);

        bool IsMine(int xPosition, int yPosition);
    }
}