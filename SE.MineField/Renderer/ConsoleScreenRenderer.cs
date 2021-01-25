using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SE.MineField.Enums;
using SE.MineField.Interfaces;
using SE.MineField.Models;

namespace SE.MineField
{
    public class ConsoleScreenRenderer : IRenderer
    {
        private IConsoleWrapper _consoleWrapper;

        public ConsoleScreenRenderer(IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;
        }

        public void DrawBoard(IGameBoard board, IPlayer player)
        {
            _consoleWrapper.Clear();
            DrawYLabels(board.YLabels.Values.AsEnumerable());

            DrawGameBoard(board, player);
        }

        public void DrawLives(IPlayer player)
        {
            _consoleWrapper.WriteLine($"Remaining lives: {player.RemainingLives}");
        }

        public void DrawScore(IPlayer player)
        {
            _consoleWrapper.WriteLine($"Score: {player.Score}");
        }

        public void DrawWinner(IPlayer player)
        {
            _consoleWrapper.WriteLine("Congratulations, you have won");
        }

        private void DrawGameBoard(IGameBoard board, IPlayer player)
        {
            for (int yPosition = 0; yPosition < board.Size; yPosition++)
            {
                _consoleWrapper.Write($"{board.XLabels[yPosition + 1]} ");

                for (int xPosition = 0; xPosition < board.Size; xPosition++)
                {
                    if (xPosition > 9)
                    {
                        _consoleWrapper.Write(" ", GetTextColor(player, xPosition, yPosition));
                    }

                    if (PlayerHasVisitedSquare(player, xPosition, yPosition))
                    {
                        switch (board.Board[xPosition, yPosition])
                        {
                            case SquareType.Free:
                                _consoleWrapper.Write(" O ", GetTextColor(player, xPosition, yPosition));
                                break;

                            case SquareType.Mine:
                                _consoleWrapper.Write(" X ", GetTextColor(player, xPosition, yPosition));
                                break;
                        }
                    }
                    else
                    {
                        _consoleWrapper.Write(" O ", GetTextColor(player, xPosition, yPosition));
                    }
                }

                _consoleWrapper.Write(Environment.NewLine);
            }
        }

        private ConsoleColor GetTextColor(IPlayer player, int xPosition, int yPosition)
        {
            var textColor = ConsoleColor.White;

            if (IsCurrentSquare(player, xPosition, yPosition))
            {
                textColor = ConsoleColor.Green;
            }
            else if (PlayerHasVisitedSquare(player, xPosition, yPosition))
            {
                textColor = ConsoleColor.Magenta;
            }

            return textColor;
        }

        private static bool PlayerHasVisitedSquare(IPlayer player, int xPosition, int yPosition)
        {
            return player.Moves.Any(w => w.XPosition == xPosition && w.YPosition == yPosition);
        }

        private static bool IsCurrentSquare(IPlayer player, int xPosition, int yPosition)
        {
            return yPosition == player.YPosition && xPosition == player.XPosition;
        }

        private void DrawYLabels(IEnumerable<string> labels)
        {
            var sb = new StringBuilder();

            sb.Append($" ");
            foreach (var label in labels)
            {
                sb.Append($"  {label}");
            }

            _consoleWrapper.WriteLine(sb.ToString());
        }
    }
}