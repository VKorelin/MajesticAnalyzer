using MajesticAnalyzer.Html;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;

namespace MajesticAnalyzer.Tests.Html
{
    [TestFixture]
    public class HtmlWrapperTest
    {
        private Uri _uri;
        private string _html;
        private IHtmlExtractor _htmlExtractor;

        public void Setup()
        {
            _uri = new Uri("http://www.test.com");
            _html = "test html";

        }

        private HtmlWrapper CreateInstance(IHtmlExtractor extractor)
        {
            return new HtmlWrapper(_uri, _html, extractor);
        }

        [Test]
        public void ExposesHtml()
        {
            var instance = CreateInstance(Mock.Of<IHtmlExtractor>());

            instance.Html.ShouldBe(_html);
        }

        [Test]
        public void ExposesUrl()
        {
            var instance = CreateInstance(Mock.Of<IHtmlExtractor>());

            instance.Url.ShouldBe(_uri);
        }

        [Test]
        public void GetsTitle()
        {
            const string expectedTitle = "test title";

            var htmlExtractorMock = new Mock<IHtmlExtractor>();
            htmlExtractorMock.Setup(x => x.ExtractTitle(It.IsAny<string>())).Returns(expectedTitle);

            var instance = CreateInstance(htmlExtractorMock.Object);

            var title = instance.GetTitle();

            title.ShouldBe(expectedTitle);
        }

        [Test]
        public void GetsDescription()
        {
            const string expectedDescription = "test description";

            var htmlExtractorMock = new Mock<IHtmlExtractor>();
            htmlExtractorMock.Setup(x => x.ExtractDescription(It.IsAny<string>())).Returns(expectedDescription);

            var instance = CreateInstance(htmlExtractorMock.Object);

            var description = instance.GetDescription();

            description.ShouldBe(expectedDescription);
        }
    }
}
