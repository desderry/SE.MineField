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
        private Mock<IGameBoard> _gameBoardMock;
        private Mock<IRenderer> _rendererMock;
        private IConsoleWrapper _consoleWrapper;

        private IGameEngine _gameEngine;

        public GameEngineTests()
        {
            _gameBoardMock = new Mock<IGameBoard>();
            _rendererMock = new Mock<IRenderer>();
            _consoleWrapper = new InMemoryConsole();
            _gameEngine = new GameEngine(_gameBoardMock.Object, _rendererMock.Object, _consoleWrapper);
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

            _rendererMock.Verify(v => v.DrawBoard(It.IsAny<GameBoard>()), Times.Once);
        }
    }
}