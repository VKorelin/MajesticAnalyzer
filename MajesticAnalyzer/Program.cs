using System;

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
            
            Console.WriteLine("Finish");
            Console.ReadKey();
        }
    }
}
