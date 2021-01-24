using System;
using System.Collections.Generic;
using System.Text;
using SE.MineField.Enums;
using SE.MineField.Models;

namespace SE.MineField.Interfaces
{
    public interface IGameBoard
    {

        public GameBoard Generate(int size);
    }
}
