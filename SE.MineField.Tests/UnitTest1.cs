using System;
using FluentAssertions;
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
            var squaresList = _gameBoard.Generate(4);

            squaresList.Should().HaveCount(16);
        }
    }
}
