using System;
using System.Collections.Generic;
using System.Text;
using SE.MineField.Enums;

namespace SE.MineField.Interfaces
{
    public interface IGameEngine
    {
        GameStatus Status { get; }

        void PlayerMoves(ConsoleKey input);

        void Start();
    }
}