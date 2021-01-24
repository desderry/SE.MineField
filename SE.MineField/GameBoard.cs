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
        public SquareType[,] Generate(int size)
        {
            return GenerateMines(new SquareType[size, size]);
        }

        private static SquareType[,] GenerateMines(SquareType[,] squares)
        {
            squares[0, 1] = SquareType.Mine;
            squares[1, 1] = SquareType.Mine;
            squares[1, 3] = SquareType.Mine;
            squares[2, 2] = SquareType.Mine;
            squares[2, 0] = SquareType.Mine;
            squares[3, 0] = SquareType.Mine;
            squares[3, 2] = SquareType.Mine;
            squares[3, 3] = SquareType.Mine;

            return squares;
        }
    }
}
