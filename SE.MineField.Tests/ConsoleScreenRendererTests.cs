using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Moq;
using SE.MineField.Enums;
using SE.MineField.Interfaces;
using SE.MineField.Models;
using Xunit;

namespace SE.MineField.Tests
{
    public class ConsoleScreenRendererTests
    {
        private InMemoryConsole _ConsoleWrapper;
        private IRenderer _renderer;
        private Mock<IGameBoard> _gameboardMock;
        private Mock<IPlayer> _playerMock;

        public ConsoleScreenRendererTests()
        {
            _gameboardMock = new Mock<IGameBoard>();
            _playerMock = new Mock<IPlayer>();
            _ConsoleWrapper = new InMemoryConsole();
            _renderer = new ConsoleScreenRenderer(_ConsoleWrapper);

            SetupGameBoardMock();

            _playerMock.Setup(s => s.Moves).Returns(new List<Move>());
        }

        private void SetupGameBoardMock()
        {
            var board = new GameBoard();
            board.Board = new SquareType[9, 9];
            board.Board[0, 2] = SquareType.Mine;
            board.Board[1, 2] = SquareType.Mine;
            board.Size = 10;
            board.YLabels = new Dictionary<int, string>()
            {
                {1,"1"},
                {2, "2"},
                {3, "3"},
                {4, "4"},
                {5, "5"},
                {6, "6"},
                {7, "7"},
                {8, "8"},
                {9, "9"},
                {10, "10"}
            };
            board.XLabels = new Dictionary<int, string>()
            {
                {1,"A"},
                {2, "B"},
                {3, "C"},
                {4, "D"},
                {5, "E"},
                {6, "F"},
                {7, "G"},
                {8, "H"},
                {9, "I"},
                {10, "J"}
            };
            _gameboardMock.Setup(s => s.Board).Returns(board.Board);
            _gameboardMock.Setup(s => s.Size).Returns(board.Size);
            _gameboardMock.Setup(s => s.YLabels).Returns(board.YLabels);
            _gameboardMock.Setup(s => s.XLabels).Returns(board.XLabels);
        }

        [Fact]
        public void WhenRenderingGameBoard_ThenRenderYAxisLabels()
        {
            var player = new Player();
            _renderer.DrawBoard(_gameboardMock.Object, player);

            _ConsoleWrapper.ConsoleOutput[0].Should().Be("   1  2  3  4  5  6  7  8  9  10");
        }

        [Fact]
        public void WhenRenderingGameBoard_NoMoves_ThenRenderGameBoard_AndAllMinesHidden()
        {
            _renderer.DrawBoard(_gameboardMock.Object, _playerMock.Object);

            _ConsoleWrapper.ConsoleOutput[1].Should().Be("A  O  O  O  O  O  O  O  O  O  O \r\n");
            _ConsoleWrapper.ConsoleOutput[2].Should().Be("B  O  O  O  O  O  O  O  O  O  O \r\n");
            _ConsoleWrapper.ConsoleOutput[3].Should().Be("C  O  O  O  O  O  O  O  O  O  O \r\n");
            _ConsoleWrapper.ConsoleOutput[4].Should().Be("D  O  O  O  O  O  O  O  O  O  O \r\n");
            _ConsoleWrapper.ConsoleOutput[5].Should().Be("E  O  O  O  O  O  O  O  O  O  O \r\n");
            _ConsoleWrapper.ConsoleOutput[6].Should().Be("F  O  O  O  O  O  O  O  O  O  O \r\n");
            _ConsoleWrapper.ConsoleOutput[7].Should().Be("G  O  O  O  O  O  O  O  O  O  O \r\n");
            _ConsoleWrapper.ConsoleOutput[8].Should().Be("H  O  O  O  O  O  O  O  O  O  O \r\n");
            _ConsoleWrapper.ConsoleOutput[9].Should().Be("I  O  O  O  O  O  O  O  O  O  O \r\n");
            _ConsoleWrapper.ConsoleOutput[10].Should().Be("J  O  O  O  O  O  O  O  O  O  O \r\n");
        }
    }
}