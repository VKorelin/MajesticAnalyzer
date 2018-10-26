using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MajesticAnalyzer.Html;
using CsvHelper.Configuration;
using System.IO;
using CsvHelper;
using MajesticAnalyzer.Domain;
using CsvHelper.TypeConversion;
using System.Net;

namespace MajesticAnalyzer
{
    class Program
    {
        private static IContainer _container;

        static void Main(string[] args)
        {
            RegisterComponents();

            _container.Resolve<IAnalyzer>().Start();

            _container?.Dispose();
            Console.ReadKey();
        }

        static void RegisterComponents()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Analyzer>().AsImplementedInterfaces();
            builder.RegisterType<ConsoleOutput>().AsImplementedInterfaces();

            //HtmlLoader
            builder.RegisterType<HtmlLoader>().AsImplementedInterfaces();
            builder.RegisterType<CqHtmlExtractor>().AsImplementedInterfaces();
            builder.RegisterType<HtmlWrapper>().AsImplementedInterfaces();
            builder.RegisterType<HtmlWrapperFactory>().AsImplementedInterfaces();
            builder.RegisterType<HtmlLoaderFactory>().AsImplementedInterfaces();
            builder.RegisterType<WebClientWrapper>().AsImplementedInterfaces();

            _container = builder.Build();
        }
    }
}
