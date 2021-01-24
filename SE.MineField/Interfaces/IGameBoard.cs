using System;
using System.Collections.Generic;
using System.Text;

namespace SE.MineField.Interfaces
{
    public interface IGameBoard
    {

        public object[,] Generate(int size);
    }
}
