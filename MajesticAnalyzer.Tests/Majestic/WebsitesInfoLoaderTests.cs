using System.Collections.Generic;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.Majestic;
using MajesticAnalyzer.Parser;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MajesticAnalyzer.Tests.Majestic
{
    [TestFixture]
    public class WebsitesInfoLoaderTests
    {
        private Mock<ICsvParser<UniversityInfo, WebsitesInfoMap>> _csvParserMock;

        private WebsitesInfoLoader CreateInstance() 
            => new WebsitesInfoLoader(_csvParserMock.Object, Mock.Of<IPathProvider>(x => x.HomeDirectory == string.Empty));

        [SetUp]
        public void Setup()
        {
            _csvParserMock = new Mock<ICsvParser<UniversityInfo, WebsitesInfoMap>>();
        }

        [Test]
        public void ParesCsvFile()
        {
            var instance = CreateInstance();
            var expected = new List<UniversityInfo>();
            _csvParserMock.Setup(x => x.Parse(It.IsAny<string>())).Returns(expected);

            var actual = instance.Load();
            
            actual.ShouldBe(expected);
        }
    }
}