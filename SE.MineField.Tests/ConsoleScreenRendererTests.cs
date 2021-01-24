﻿using System;
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

        public ConsoleScreenRendererTests()
        {
            _ConsoleWrapper = new InMemoryConsole();
            _renderer = new ConsoleScreenRenderer(_ConsoleWrapper);
        }

        [Fact]
        public void WhenRenderingGameBoard_ThenRenderYAxisLabels()
        {
            var board = new GameBoard();
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
            _renderer.DrawBoard(board);

            _ConsoleWrapper.ConsoleOutput[0].Should().Be(" 1 2 3 4 5 6 7 8 9 10");
        }

        [Fact]
        public void WhenRenderingGameBoard_ThenRenderGameBoard()
        {
            var board = new GameBoard();
            board.Board = new SquareType[2, 2];
            board.Size = 2;
            board.YLabels = new Dictionary<int, string>()
            {
                {1,"1"},
                {2, "2"}
            };
            board.XLabels = new Dictionary<int, string>()
            {
                {1,"A"},
                {2, "B"}
            };
            _renderer.DrawBoard(board);

            _ConsoleWrapper.ConsoleOutput[1].Should().Be("A  O  O ");
            _ConsoleWrapper.ConsoleOutput[2].Should().Be("B  O  O ");
        }
    }
}