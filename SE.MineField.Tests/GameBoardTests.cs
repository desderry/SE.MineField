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

        [Fact] 
        public void WhenGameboardIsGenerated_ThenNumberOfSquaresGeneratedEqualsSquareOfSize()
        {
            var squaresList = _gameBoard.Generate();

            squaresList.Should().HaveCount(16);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(2, 3)]
        [InlineData(2, 4)]
        [InlineData(3, 1)]
        [InlineData(3, 2)]
        [InlineData(3, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 1)]
        [InlineData(4, 2)]
        [InlineData(4, 3)]
        [InlineData(4, 4)]

        public void WhenGameBoardIsGenerated_ThenArrayIndexesMatchGridPosition(int xPosition, int yPosition )
        {
            var squaresList = _gameBoard.Generate();

            squaresList[xPosition - 1, yPosition - 1].Should().BeOfType<SquareType>();
        }

        [Theory]
        [InlineData(8)]
        public void WhenGameboardIsGenerated_ThenHalfOfSquaresShouldBeMines(int countOfMines)
        {
            var squaresList = _gameBoard.Generate();

            squaresList.ToList().Count(w => w == SquareType.Mine).Should().Be(countOfMines, "Half of the total number of squares should be mines");
        }
    }
}
