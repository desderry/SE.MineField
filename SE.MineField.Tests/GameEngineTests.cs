using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using SE.MineField.Interfaces;
using SE.MineField.Models;
using Xunit;

namespace SE.MineField.Tests
{
    public class GameEngineTests
    {
        private Mock<IGameBoardService> _gameBoardMock;
        private Mock<IRenderer> _rendererMock;
        private Mock<IPlayer> _playerMock;
        private InMemoryConsole _consoleWrapper;
        private int _gameBoardSize = 12;
        private IGameEngine _gameEngine;

        public GameEngineTests()
        {
            _gameBoardMock = new Mock<IGameBoardService>();
            _rendererMock = new Mock<IRenderer>();
            _consoleWrapper = new InMemoryConsole();
            _playerMock = new Mock<IPlayer>();

            SetupPlayerMocks();
            SetupGameBoardMocks();
            _gameEngine = new GameEngine(_gameBoardMock.Object, _rendererMock.Object, _consoleWrapper, _playerMock.Object);
        }

        private void SetupGameBoardMocks()
        {
            _gameBoardMock.Setup(s => s.Generate(It.IsAny<int>())).Returns(new GameBoard()
            {
                Size = _gameBoardSize,
            });
        }

        private void SetupPlayerMocks()
        {
            _playerMock.Setup(s => s.XPosition).Returns(0);
        }

        [Fact]
        public void WhenGameStarts_ThenGenerateGameBoard()
        {
            _gameEngine.Start();

            _gameBoardMock.Verify(v => v.Generate(12), Times.Once);
        }

        [Fact]
        public void WhenGameStarts_ThenRenderGameBoard()
        {
            _gameEngine.Start();

            _rendererMock.Verify(v => v.DrawBoard(It.IsAny<GameBoard>(), It.IsAny<Player>()), Times.Once);
        }

        [Fact]
        public void WhenGameStarts_ThenRenderLives()
        {
            _gameEngine.Start();

            _rendererMock.Verify(v => v.DrawLives(It.IsAny<Player>()), Times.Once);
        }

        [Fact]
        public void WhenGameStarts_ThenRenderScore()
        {
            _gameEngine.Start();

            _rendererMock.Verify(v => v.DrawScore(It.IsAny<Player>()), Times.Once);
        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow)]
        [InlineData(ConsoleKey.DownArrow)]
        [InlineData(ConsoleKey.UpArrow)]
        [InlineData(ConsoleKey.LeftArrow)]
        public void WhenPlayerInputsMove_ThenValidatePosition(ConsoleKey input)
        {
            _gameEngine.Start();

            _gameEngine.PlayerMoves(input);

            _gameBoardMock.Verify(v => v.IsValidSquare(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow)]
        [InlineData(ConsoleKey.DownArrow)]
        [InlineData(ConsoleKey.UpArrow)]
        [InlineData(ConsoleKey.LeftArrow)]
        public void WhenPlayerInputsMove_AndMoveIsValid_ThenSetPlayerPosition(ConsoleKey input)
        {
            _gameBoardMock.Setup(s => s.IsValidSquare(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _gameEngine.Start();
            _gameEngine.PlayerMoves(input);

            _playerMock.Verify(v => v.SetPosition(It.IsAny<int>(), It.IsAny<int>()));
        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow)]
        [InlineData(ConsoleKey.DownArrow)]
        [InlineData(ConsoleKey.UpArrow)]
        [InlineData(ConsoleKey.LeftArrow)]
        public void WhenPlayerInputsMove_AndMoveIsValid_ThenCheckIfHitMine(ConsoleKey input)
        {
            _gameBoardMock.Setup(s => s.IsValidSquare(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            _gameEngine.Start();
            _gameEngine.PlayerMoves(input);

            _gameBoardMock.Verify(s => s.IsMine(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow)]
        [InlineData(ConsoleKey.DownArrow)]
        [InlineData(ConsoleKey.UpArrow)]
        [InlineData(ConsoleKey.LeftArrow)]
        public void WhenPlayerInputsMove_AndMoveIsValid_AndHitMine_ThenPlayerHitMine(ConsoleKey input)
        {
            _gameBoardMock.Setup(s => s.IsValidSquare(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _gameBoardMock.Setup(s => s.IsMine(It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            _gameEngine.Start();
            _gameEngine.PlayerMoves(input);

            _playerMock.Verify(v => v.HitMine(), Times.Once);
        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow)]
        [InlineData(ConsoleKey.DownArrow)]
        [InlineData(ConsoleKey.UpArrow)]
        [InlineData(ConsoleKey.LeftArrow)]
        public void WhenPlayerInputsMove_AndMoveIsValid_AndDidntHitMine_ThenPlayerDoesntHitMine(ConsoleKey input)
        {
            _gameBoardMock.Setup(s => s.IsValidSquare(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _gameBoardMock.Setup(s => s.IsMine(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            _gameEngine.Start();
            _gameEngine.PlayerMoves(input);

            _playerMock.Verify(v => v.HitMine(), Times.Never);
        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow)]
        [InlineData(ConsoleKey.DownArrow)]
        [InlineData(ConsoleKey.UpArrow)]
        [InlineData(ConsoleKey.LeftArrow)]
        public void WhenPlayerInputsMove_AndMoveIsValid_ThenDrawBoard(ConsoleKey input)
        {
            _gameBoardMock.Setup(s => s.IsValidSquare(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _gameBoardMock.Setup(s => s.IsMine(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            _gameEngine.Start();
            _gameEngine.PlayerMoves(input);

            _rendererMock.Verify(v => v.DrawBoard(It.IsAny<IGameBoard>(), It.IsAny<IPlayer>()), Times.Once);
        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow)]
        [InlineData(ConsoleKey.DownArrow)]
        [InlineData(ConsoleKey.UpArrow)]
        [InlineData(ConsoleKey.LeftArrow)]
        public void WhenPlayerInputsMove_AndMoveIsValid_ThenDrawLives(ConsoleKey input)
        {
            _gameBoardMock.Setup(s => s.IsValidSquare(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _gameBoardMock.Setup(s => s.IsMine(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            _gameEngine.Start();
            _gameEngine.PlayerMoves(input);

            _rendererMock.Verify(v => v.DrawLives(It.IsAny<IPlayer>()), Times.Once);
        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow)]
        [InlineData(ConsoleKey.DownArrow)]
        [InlineData(ConsoleKey.UpArrow)]
        [InlineData(ConsoleKey.LeftArrow)]
        public void WhenPlayerInputsMove_AndMoveIsValid_ThenDrawScore(ConsoleKey input)
        {
            _gameBoardMock.Setup(s => s.IsValidSquare(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _gameBoardMock.Setup(s => s.IsMine(It.IsAny<int>(), It.IsAny<int>())).Returns(false);

            _gameEngine.Start();
            _gameEngine.PlayerMoves(input);

            _rendererMock.Verify(v => v.DrawScore(It.IsAny<IPlayer>()), Times.Once);
        }

        [Theory]
        [InlineData(ConsoleKey.RightArrow)]
        [InlineData(ConsoleKey.DownArrow)]
        [InlineData(ConsoleKey.UpArrow)]
        [InlineData(ConsoleKey.LeftArrow)]
        public void WhenPlayerInputsMove_AndMoveIsValid_AndPlayerHasWon_ThenDrawScore(ConsoleKey input)
        {
            _gameBoardMock.Setup(s => s.IsValidSquare(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _gameBoardMock.Setup(s => s.IsMine(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            _playerMock.Setup(s => s.XPosition).Returns(_gameBoardSize - 1);
            _gameEngine.Start();
            _gameEngine.PlayerMoves(input);

            _rendererMock.Verify(v => v.DrawWinner(It.IsAny<IPlayer>()), Times.AtLeast(1));
        }
    }
}