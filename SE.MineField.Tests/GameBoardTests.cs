using System;
using System.Collections;
using System.Linq;
using FluentAssertions;
using SE.MineField.Enums;
using SE.MineField.Interfaces;
using Xunit;

namespace SE.MineField.Tests
{
    public class GameBoardTests
    {
        private IGameBoard _gameBoard;

        public GameBoardTests()
        {
            _gameBoard = new GameBoard();
        }

        [Theory]
        [InlineData(4, 16)]
        public void WhenGameboardIsGenerated_ThenNumberOfSquaresGeneratedEqualsSquareOfSize(int size, int noOfSquares)
        {
            var squaresList = _gameBoard.Generate(size);

            squaresList.Should().HaveCount(noOfSquares);
        }

        [Theory]
        [InlineData(4, 1, 1)]
        [InlineData(4, 1, 2)]
        [InlineData(4, 1, 3)]
        [InlineData(4, 1, 4)]
        [InlineData(4, 2, 1)]
        [InlineData(4, 2, 2)]
        [InlineData(4, 2, 3)]
        [InlineData(4, 2, 4)]
        [InlineData(4, 3, 1)]
        [InlineData(4, 3, 2)]
        [InlineData(4, 3, 3)]
        [InlineData(4, 3, 4)]
        [InlineData(4, 4, 1)]
        [InlineData(4, 4, 2)]
        [InlineData(4, 4, 3)]
        [InlineData(4, 4, 4)]

        public void WhenGameBoardIsGenerated_ThenArrayIndexesMatchGridPosition(int size, int xPosition, int yPosition)
        {
            var squaresList = _gameBoard.Generate(size);

            squaresList[xPosition - 1, yPosition - 1].Should().BeOfType<SquareType>();
        }

        [Theory]
        [InlineData(4, 8)]
        public void WhenGameboardIsGenerated_ThenHalfOfSquaresShouldBeMines(int size, int countOfMines)
        {
            var squaresList = _gameBoard.Generate(size);

            squaresList.ToList().Count(w => w == SquareType.Mine).Should().Be(countOfMines, "Half of the total number of squares should be mines");
        }
    }
}
