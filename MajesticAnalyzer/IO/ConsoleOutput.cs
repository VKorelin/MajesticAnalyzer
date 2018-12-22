using System;

namespace MajesticAnalyzer.IO
{
    public class ConsoleOutput : IOutputService
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}