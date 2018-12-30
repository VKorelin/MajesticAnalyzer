using System;
using System.Linq;
using Autofac;
using MajesticAnalyzer.Html;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.IO.Csv;
using MajesticAnalyzer.Majestic;

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
            var universities = _container.Resolve<IUniversitiesLoaderService>().LoadUniversities();

            var reffResource = universities.First().ReffResources.First(x => x.Domain != null);

            reffResource.Title = "MyTitle";
            reffResource.Description = "MyDescription";

            var outputService = _container.Resolve<IReffContentOutputService>();
            outputService.WriteContent(universities.First());
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
            builder.RegisterType<PathProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ReffContentOutputService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterGeneric(typeof(CsvHandler<,>)).AsImplementedInterfaces().SingleInstance();

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