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
            var squares = new SquareType[size, size];
            return GenerateMines(squares, size);
        }

        private static SquareType[,] GenerateMines(SquareType[,] squares, int size)
        {

            var noOfMines = size * size / 2;
            var rndIndex = new Random(noOfMines);

            while (noOfMines != 0)
            {
                var xPosition = rndIndex.Next(size - 1);
                var yPosition = rndIndex.Next(size - 1);

                var square = squares[xPosition, yPosition];

                if (square == SquareType.Free)
                {
                    squares[xPosition, yPosition] = SquareType.Mine;
                    noOfMines--;
                }
            }
            return squares;
        }
    }
}
