using System;
using System.Collections.Generic;
using System.Text;

namespace SE.MineField.Interfaces
{
    public interface IGameBoard
    {

        public IEnumerable<string> Generate(int size);
    }
}
