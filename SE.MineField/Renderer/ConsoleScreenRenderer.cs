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
        private const string _spacing = "  ";

        public ConsoleScreenRenderer(IConsoleWrapper consoleWrapper)
        {
            _consoleWrapper = consoleWrapper;
        }

        public void DrawBoard(GameBoard board)
        {
            DrawYLabels(board.YLabels.Values.AsEnumerable());

            DrawGameBoard(board);
        }

        private void DrawGameBoard(GameBoard board)
        {
            for (int yPosition = 0; yPosition < board.Size; yPosition++)
            {
                var sb = new StringBuilder();
                sb.Append($"{board.XLabels[yPosition + 1]} ");

                for (int xPosition = 0; xPosition < board.Size; xPosition++)
                {
                    if (xPosition > 9)
                    {
                        sb.Append(" ");
                    }
                    switch (board.Board[yPosition, xPosition])
                    {
                        case SquareType.Free:
                            sb.Append(" O ");
                            break;

                        case SquareType.Mine:
                            sb.Append(" X ");
                            break;
                    }
                }
                _consoleWrapper.WriteLine(sb.ToString());
            }
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