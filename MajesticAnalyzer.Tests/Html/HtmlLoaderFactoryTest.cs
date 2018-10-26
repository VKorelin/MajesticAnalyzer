using MajesticAnalyzer.Html;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;

namespace MajesticAnalyzer.Tests.Html
{
    [TestFixture]
    public class HtmlLoaderFactoryTest
    {
        private HtmlLoaderFactory CreateInstance(Func<IHtmlLoader> factory)
        {
            return new HtmlLoaderFactory(factory);
        }

        [Test]
        public void CreatesHtmlLoader()
        {
            var expectedLoader = new HtmlLoader(Mock.Of<IHtmlWrapperFactory>(), Mock.Of<IWebClientWrapper>());

            var loaderFactory = CreateInstance(() => expectedLoader);

            var loader = loaderFactory.Create();

            loader.ShouldBeSameAs(expectedLoader);
        }
    }
}
