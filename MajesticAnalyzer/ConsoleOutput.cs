using System;

namespace MajesticAnalyzer
{
    public class ConsoleOutput : IOutputService
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}