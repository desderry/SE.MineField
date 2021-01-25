using System;
using System.Collections.Generic;
using System.Text;
using SE.MineField.Enums;
using SE.MineField.Models;

namespace SE.MineField.Interfaces
{
    public interface IGameBoardService
    {
        public GameBoard Generate(int size);

        bool IsValidSquare(int playerXPosition, int playerYPosition);

        bool IsMine(int xPosition, int yPosition);
    }
}