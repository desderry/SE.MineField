using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SE.MineField.Interfaces;

namespace SE.MineField.Tests
{
    public class InMemoryConsole : IConsoleWrapper
    {
        public IList<string> ConsoleOutput = new List<string>();
        
        public void WriteLine(string line)
        {
            ConsoleOutput.Add(line);
        }
    }
}
