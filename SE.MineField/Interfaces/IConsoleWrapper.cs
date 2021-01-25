using System;
using System.Collections.Generic;
using System.Text;

namespace SE.MineField.Interfaces
{
    public interface IConsoleWrapper
    {
        void WriteLine(string line);

        void Write(string content, ConsoleColor color = ConsoleColor.White);

        ConsoleKeyInfo ReadKey();

        void Clear();
    }
}