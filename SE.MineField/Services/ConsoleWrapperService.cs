using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace SE.MineField.Interfaces
{
    public class ConsoleWrapperService : IConsoleWrapper
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}