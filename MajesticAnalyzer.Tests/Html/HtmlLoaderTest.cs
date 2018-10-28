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

        private Mock<IHtmlWrapperFactory> _htmlWrapperFactoryMock;

        private Mock<IWebClientWrapper> _webClientWrapperMock;

        [SetUp]
        public void Setup()
        {
            _htmlWrapperFactoryMock = new Mock<IHtmlWrapperFactory>();
            _webClientWrapperMock = new Mock<IWebClientWrapper>();
        }

        private HtmlLoader CreateInstance()
        {
            return new HtmlLoader(_htmlWrapperFactoryMock.Object, _webClientWrapperMock.Object);
        }

        [Test]
        public void LoadsHtmlViaWebClientWrapperOnce()
        {
            var htmlLoader = CreateInstance();

            htmlLoader.Load(Uri);

            _webClientWrapperMock.Verify(x => x.Load(Uri), Times.Once);
        }

        [Test]
        public void CreatesHtmlWrapperOnceOnLoad()
        {
            var htmlLoader = CreateInstance();

            htmlLoader.Load(Uri);

            _htmlWrapperFactoryMock.Verify(
                x => x.Create(new Uri(Uri), It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void LoadHtml()
        {
            var expectedWrapper = new HtmlWrapper(new Uri(Uri), "test html", Mock.Of<IHtmlExtractor>());

            _htmlWrapperFactoryMock
                .Setup(x => x.Create(It.IsAny<Uri>(), It.IsAny<string>()))
                .Returns(expectedWrapper);

            var htmlLoader = CreateInstance();

            var htmlWrapper = htmlLoader.Load(Uri);

            htmlWrapper.ShouldBe(expectedWrapper);
        }
    }
}
