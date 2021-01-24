using System;
using System.Collections;
using System.Collections.Generic;
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
            _gameBoard = new GameBoardService();
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(11)]
        public void WhenGameBoardIsGenerated_AndSizeIsOddNumber_ThenThrowArgumentException(int size)
        {
            Action act = () => _gameBoard.Generate(size);

            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(4, 16)]
        [InlineData(6, 36)]
        [InlineData(12, 144)]
        public void WhenGameboardIsGenerated_ThenNumberOfSquaresGeneratedEqualsSquareOfSize(int size, int noOfSquares)
        {
            var board = _gameBoard.Generate(size);

            board.Board.Should().HaveCount(noOfSquares);
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
        [InlineData(6, 6, 6)]
        [InlineData(12, 6, 12)]
        [InlineData(12, 12, 6)]
        [InlineData(12, 12, 12)]
        public void WhenGameBoardIsGenerated_ThenArrayIndexesMatchGridPosition(int size, int xPosition, int yPosition)
        {
            var board = _gameBoard.Generate(size);

            board.Board[xPosition - 1, yPosition - 1].Should().BeOfType<SquareType>();
        }

        [Theory]
        [InlineData(4, 8)]
        [InlineData(6, 18)]
        [InlineData(8, 32)]
        [InlineData(12, 72)]
        public void WhenGameboardIsGenerated_ThenHalfOfSquaresShouldBeMines(int size, int countOfMines)
        {
            var board = _gameBoard.Generate(size);

            board.Board.ToList().Count(w => w == SquareType.Mine).Should().Be(countOfMines, "Half of the total number of squares should be mines");
        }

        [Theory]
        [MemberData(nameof(GetYLabels))]
        public void WhenGameboardIsGenerated_ThenYLabelsShouldBeGenerated(int size, Dictionary<int, string> labels)
        {
            var board = _gameBoard.Generate(size);

            board.YLabels.Should().NotBeNull();
            board.YLabels.Should().NotBeEmpty();
            board.YLabels.Should().BeEquivalentTo(labels);
        }

        [Theory]
        [MemberData(nameof(GetXLabels))]
        public void WhenGameboardIsGenerated_ThenXLabelsShouldBeGenerated(int size, Dictionary<int, string> labels)
        {
            var board = _gameBoard.Generate(size);

            board.XLabels.Should().NotBeNull();
            board.XLabels.Should().NotBeEmpty();
            board.XLabels.Should().BeEquivalentTo(labels);
        }

        public static IEnumerable<object[]> GetYLabels()
        {
            yield return new object[] { 4, new Dictionary<int, string>() { { 1, "1" }, { 2, "2" }, { 3, "3" }, { 4, "4" } } };
            yield return new object[] { 6, new Dictionary<int, string>() { { 1, "1" }, { 2, "2" }, { 3, "3" }, { 4, "4" }, { 5, "5" }, { 6, "6" } } };
        }

        public static IEnumerable<object[]> GetXLabels()
        {
            yield return new object[] { 4, new Dictionary<int, string>() { { 1, "A" }, { 2, "B" }, { 3, "C" }, { 4, "D" } } };
            yield return new object[] { 6, new Dictionary<int, string>() { { 1, "A" }, { 2, "B" }, { 3, "C" }, { 4, "D" }, { 5, "E" }, { 6, "F" } } };
        }

        [Theory]
        [InlineData(4, 1, 4)]
        [InlineData(4, 2, 4)]
        [InlineData(4, 3, 4)]
        [InlineData(4, 4, 4)]
        [InlineData(4, 1, 1)]
        [InlineData(4, 2, 1)]
        [InlineData(4, 3, 1)]
        [InlineData(4, 4, 1)]
        public void WhenPositionIsValidated_AndSquareExists_ThenReturnTrue(int size, int xPos, int yPos)
        {
            _gameBoard.Generate(size);

            var valid = _gameBoard.IsValidSquare(xPos, yPos);

            valid.Should().BeTrue();
        }

        [Theory]
        [InlineData(4, 2, 5)]
        [InlineData(4, 2, 0)]
        [InlineData(4, 0, 1)]
        [InlineData(4, 5, 1)]
        public void WhenPositionIsValidated_AndSquareDoesnt_ThenReturnFalse(int size, int xPos, int yPos)
        {
            _gameBoard.Generate(size);

            var valid = _gameBoard.IsValidSquare(xPos, yPos);

            valid.Should().BeFalse();
        }
    }
}