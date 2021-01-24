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
        private SquareType[,] _squares;

        public SquareType[,] Generate(int size)
        {
            if (size % 2 != 0)
            {
                throw new ArgumentException("Odd numbers are not valid for board size");
            }

            _squares = new SquareType[size, size];
            GenerateMines(size);

            return _squares;
        }

        private void GenerateMines(int size)
        {

            var noOfMines = size * size / 2;
            var rndIndex = new Random(noOfMines);

            while (noOfMines != 0)
            {
                var xPosition = rndIndex.Next(size - 1);
                var yPosition = rndIndex.Next(size - 1);

                var square = _squares[xPosition, yPosition];

                if (square == SquareType.Free)
                {
                    _squares[xPosition, yPosition] = SquareType.Mine;
                    noOfMines--;
                }
            }
        }
    }
}
