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
        private IGameBoardService _gameBoardService;
        private IRenderer _renderer;
        private IConsoleWrapper _consoleWrapper;
        private IPlayer _player;
        private GameBoard _board;

        public GameEngine(IGameBoardService gameBoardService, IRenderer renderer, IConsoleWrapper consoleWrapper, IPlayer player)
        {
            _gameBoardService = gameBoardService;
            _renderer = renderer;
            _consoleWrapper = consoleWrapper;
            _player = player;
        }

        public void Start()
        {
            var boardSize = 16;
            _board = _gameBoardService.Generate(boardSize);

            SetPlayerStartPosition(boardSize);
            RenderGame();
        }

        private void SetPlayerStartPosition(int boardSize)
        {
            _player.SetPosition(0, new Random().Next(boardSize));
        }

        public GameStatus Status => GetStatus();

        private GameStatus GetStatus()
        {
            if (_player.XPosition == _board.Size - 1)
            {
                return GameStatus.Won;
            }

            if (_player.IsAlive)
            {
                return GameStatus.InProgress;
            }

            return GameStatus.Finished;
        }

        public void PlayerMoves(ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;

                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;

                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;

                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
            }

            RenderGame();
        }

        private void CalculateMineHitorMiss()
        {
            if (_gameBoardService.IsMine(_player.XPosition, _player.YPosition))
            {
                _player.HitMine();
            }
        }

        private void RenderGame()
        {
            _renderer.DrawBoard(_board, _player);
            _renderer.DrawLives(_player);
            _renderer.DrawScore(_player);

            if (Status == GameStatus.Won)
            {
                _renderer.DrawWinner(_player);
            }
        }

        public void MoveUp()
        {
            var newPosition = _player.YPosition - 1;
            MovePlayer(_player.XPosition, newPosition);
        }

        public void MoveDown()
        {
            var newPosition = _player.YPosition + 1;
            MovePlayer(_player.XPosition, newPosition);
        }

        public void MoveLeft()
        {
            var newPosition = _player.XPosition - 1;
            MovePlayer(newPosition, _player.YPosition);
        }

        public void MoveRight()
        {
            var newPosition = _player.XPosition + 1;
            MovePlayer(newPosition, _player.YPosition);
        }

        private void MovePlayer(int xPosition, int yPosition)
        {
            if (_gameBoardService.IsValidSquare(xPosition, yPosition))
            {
                _player.SetPosition(xPosition, yPosition);

                CalculateMineHitorMiss();
            }
        }
    }
}