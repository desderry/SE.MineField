using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SE.MineField.Interfaces;

namespace SE.MineField
{
    public class GameBoard : IGameBoard
    {
        public IEnumerable<string> Generate(int size)
        {
            var collectionSize = size * size;
            return new string[collectionSize].ToList();
        }
    }
}
