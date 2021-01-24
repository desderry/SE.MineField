using System;
using System.Collections.Generic;
using System.Text;
using SE.MineField.Enums;

namespace SE.MineField.Models
{
    public class GameBoard
    {
        public SquareType[,] Board { get; set; }
        public int Size { get; set; }
        public Dictionary<int, string> YLabels { get; set; }
        public Dictionary<int, string> XLabels { get; set; }
    }
}