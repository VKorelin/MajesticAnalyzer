using Autofac;
using System;
using MajesticAnalyzer.Html;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.Majestic;
using MajesticAnalyzer.Parser;

namespace MajesticAnalyzer
{
    public class Program
    {
        // ReSharper disable once UnusedParameter.Local
        public static void Main(string[] args)
        {
            using (var bootstrapper = new Bootstrapper())
            {
                bootstrapper.Run();
                bootstrapper.LoadUniversities();
            }
            
            Console.ReadKey();
        }
    }
}
