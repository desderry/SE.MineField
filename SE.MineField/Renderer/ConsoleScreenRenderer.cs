using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void DrawBoard(GameBoard board)
        {
            _consoleWrapper.WriteLine(DrawYLabels(board.YLabels.Values.AsEnumerable()).ToString());
        }

        private static StringBuilder DrawYLabels(IEnumerable<string> labels)
        {
            var sb = new StringBuilder();

            foreach (var label in labels)
            {
                sb.Append($" {label}");
            }

            return sb;
        }
    }
}