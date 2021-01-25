using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SE.MineField.Interfaces;

namespace SE.MineField.Tests
{
    public class InMemoryConsole : IConsoleWrapper
    {
        public IList<string> ConsoleLineOutput = new List<string>();

        public IList<string> ConsoleOutput = new List<string>();
        private ConsoleKeyInfo _inputKey;

        public void WriteLine(string line)
        {
            ConsoleOutput.Add(line);
        }

        public void Write(string content, ConsoleColor color = ConsoleColor.White)
        {
            ConsoleLineOutput.Add(content);

            if (content.Contains("\n"))
            {
                ConsoleOutput.Add(String.Join(string.Empty, ConsoleLineOutput));
                ConsoleLineOutput.Clear();
            }
        }

        public ConsoleKeyInfo ReadKey()
        {
            return _inputKey;
        }

        public void Clear()
        {
            //dont need to do anything here
        }

        public void SetInputKey(ConsoleKey key)
        {
            _inputKey = new ConsoleKeyInfo(' ', key, false, false, false);
        }
    }
}