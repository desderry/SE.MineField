using System;
using System.Collections.Generic;
using System.Text;
using SE.MineField.Interfaces;

namespace SE.MineField
{
    public class GameEngine : IGameEngine
    {
        private IGameBoard _gameBoard;
        private IRenderer _renderer;

        public GameEngine(IGameBoard gameBoard, IRenderer renderer)
        {
            _gameBoard = gameBoard;
            _renderer = renderer;
        }

        public void Start()
        {
            var board = _gameBoard.Generate(12);

            _renderer.DrawBoard(board);
        }
    }
}