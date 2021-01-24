using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SE.MineField.Enums;
using SE.MineField.Interfaces;

namespace SE.MineField
{
    public class GameBoard : IGameBoard
    {
        public SquareType[,] Generate()
        {
            return new SquareType[4, 4];
        }
    }
}
