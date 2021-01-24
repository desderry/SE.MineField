using System;
using System.Collections.Generic;
using System.Text;
using SE.MineField.Enums;
using SE.MineField.Interfaces;
using SE.MineField.Models;

namespace SE.MineField
{
    public class GameEngine : IGameEngine
    {
        private IGameBoard _gameBoard;
        private IRenderer _renderer;
        private IConsoleWrapper _consoleWrapper;

        public GameEngine(IGameBoard gameBoard, IRenderer renderer, IConsoleWrapper consoleWrapper)
        {
            _gameBoard = gameBoard;
            _renderer = renderer;
            _consoleWrapper = consoleWrapper;
        }

        public void Start()
        {
            var board = _gameBoard.Generate(12);

            _renderer.DrawBoard(board);
        }
    }
}