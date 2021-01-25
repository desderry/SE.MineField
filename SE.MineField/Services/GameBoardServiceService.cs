using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using SE.MineField.Enums;
using SE.MineField.Interfaces;
using SE.MineField.Models;

namespace SE.MineField
{
    public class GameBoardServiceService : IGameBoardService
    {
        private GameBoard _board;

        public GameBoard Generate(int size)
        {
            if (size % 2 != 0)
            {
                throw new ArgumentException("Odd numbers are not valid for board size");
            }

            _board = new GameBoard()
            {
                Size = size,
                Board = GenerateMines(size),
                YLabels = GenerateLabels(size),
                XLabels = GenerateLabels(size, true)
            };

            return _board;
        }

        public bool IsValidSquare(int xPosition, int yPosition)
        {
            if (xPosition < 0 || yPosition < 0 || xPosition >= _board.Size || yPosition >= _board.Size)
            {
                return false;
            }

            return true;
        }

        public bool IsMine(int xPosition, int yPosition)
        {
            return _board.Board[xPosition, yPosition] == SquareType.Mine;
        }

        private SquareType[,] GenerateMines(int size)
        {
            var _squares = new SquareType[size, size];

            var noOfMines = size * size / 3;
            var rndIndex = new Random(noOfMines);

            while (noOfMines != 0)
            {
                var xPosition = rndIndex.Next(size);
                var yPosition = rndIndex.Next(size);

                var square = _squares[xPosition, yPosition];

                if (square == SquareType.Free)
                {
                    _squares[xPosition, yPosition] = SquareType.Mine;
                    noOfMines--;
                }
            }

            return _squares;
        }

        private Dictionary<int, string> GenerateLabels(int size, bool useAlpha = false)
        {
            var labels = new Dictionary<int, string>();

            for (int i = 1; i < size + 1; i++)
            {
                if (useAlpha)
                {
                    labels.Add(i, GetLabelName(i));
                }
                else
                {
                    labels.Add(i, i.ToString());
                }
            }

            return labels;
        }

        private string GetLabelName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";
            index--;

            if (index >= letters.Length)
                throw new ArgumentOutOfRangeException($"Unable to get label for index {index}");

            value += letters[index % letters.Length];

            return value;
        }
    }
}