using System;
using Autofac;
using MajesticAnalyzer.Html;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.Majestic;
using MajesticAnalyzer.Parser;

namespace MajesticAnalyzer
{
    public class Bootstrapper : IDisposable
    {
        private IContainer _container;
        
        public void Run()
        {
            _container = InitializeContainer();
        }

        public void LoadUniversities()
        {
            _container.Resolve<IUniversitiesLoaderService>().LoadUniversities();
        }

        private IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UniversitiesLoaderService>().AsImplementedInterfaces().SingleInstance();
            
            //Majestic
            builder.RegisterType<WebsitesInfoLoader>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<BacklinksLoader>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<RefdomainsLoader>().AsImplementedInterfaces().SingleInstance();
            
            //IO
            builder.RegisterType<ConfigurationProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConsoleOutput>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<PathProvider>().AsImplementedInterfaces().SingleInstance();
            
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

        public void Dispose() => _container?.Dispose();
    }
}