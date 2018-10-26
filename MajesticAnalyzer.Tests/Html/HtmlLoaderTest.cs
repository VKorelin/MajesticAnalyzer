using MajesticAnalyzer.Html;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;

namespace MajesticAnalyzer.Tests.Html
{
    [TestFixture]
    public class HtmlLoaderTest
    {
        private const string Uri = "http://www.test.com";

        private Mock<IHtmlWrapperFactory> htmlWrapperFactoryMock;

        private Mock<IWebClientWrapper> webClientWrapperMock;

        [SetUp]
        public void Setup()
        {
            htmlWrapperFactoryMock = new Mock<IHtmlWrapperFactory>();
            webClientWrapperMock = new Mock<IWebClientWrapper>();
        }

        private HtmlLoader CreateInstance()
        {
            return new HtmlLoader(htmlWrapperFactoryMock.Object, webClientWrapperMock.Object);
        }

        [Test]
        public void LoadsHtmlViaWebClientWrapperOnce()
        {
            var htmlLoader = CreateInstance();

            htmlLoader.Load(Uri);

            webClientWrapperMock.Verify(x => x.Load(Uri), Times.Once);
        }

        [Test]
        public void CreatesHtmlWrapperOnceOnLoad()
        {
            var htmlLoader = CreateInstance();

            htmlLoader.Load(Uri);

            htmlWrapperFactoryMock.Verify(
                x => x.Create(new Uri(Uri), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void LoadHtml()
        {
            var expectedWrapper = new HtmlWrapper(new Uri(Uri), "test html", Mock.Of<IHtmlExtractor>());

            htmlWrapperFactoryMock
                .Setup(x => x.Create(It.IsAny<Uri>(), It.IsAny<string>()))
                .Returns(expectedWrapper);

            var htmlLoader = CreateInstance();

            var htmlWrapper = htmlLoader.Load(Uri);

            htmlWrapper.ShouldBe(expectedWrapper);
        }
    }
}
