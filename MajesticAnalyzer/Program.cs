using Autofac;
using System;
using MajesticAnalyzer.Html;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.Majestic;
using MajesticAnalyzer.Parser;

namespace MajesticAnalyzer
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] args)
        {
            using (var container = RegisterComponents())
            {
                container.Resolve<IAnalyzer>().Start();
                container?.Dispose();
            }
            
            Console.ReadKey();
        }

        static IContainer RegisterComponents()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Analyzer>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConsoleOutput>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<PathProvider>().AsImplementedInterfaces().SingleInstance();
            
            //Majestic
            builder.RegisterType<WebsitesInfoLoader>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<BacklinksLoader>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<RefdomainsLoader>().AsImplementedInterfaces().SingleInstance();
            
            //Parser
            builder.RegisterGeneric(typeof(CsvParser<,>)).AsImplementedInterfaces().SingleInstance();

            //HtmlLoader
            builder.RegisterType<HtmlLoader>().AsImplementedInterfaces();
            builder.RegisterType<CqHtmlExtractor>().AsImplementedInterfaces();
            builder.RegisterType<HtmlWrapper>().AsImplementedInterfaces();
            builder.RegisterType<HtmlWrapperFactory>().AsImplementedInterfaces();
            builder.RegisterType<HtmlLoaderFactory>().AsImplementedInterfaces();
            builder.RegisterType<WebClientWrapper>().AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
