using MajesticAnalyzer.Domain;
using MajesticAnalyzer.Majestic;
using MajesticAnalyzer.Parser;
using Moq;
using NUnit.Framework;

namespace MajesticAnalyzer.Tests.Majestic
{
    [TestFixture]
    public class WebsitesInfoParserTest
    {
        private Mock<ICsvParser<UniversityInfo, WebsitesInfoMap>> _csvParserMock;
        private Mock<IPathProvider> _pathProviderMock;

        private WebsitesInfoLoader CreateInstance() => new WebsitesInfoLoader(_csvParserMock.Object, _pathProviderMock.Object);

        [SetUp]
        public void Setup()
        {
            _csvParserMock = new Mock<ICsvParser<UniversityInfo, WebsitesInfoMap>>();
            _pathProviderMock = new Mock<IPathProvider>();
        }

        [Test]
        public void ParesCsvFile()
        {
            var instance = CreateInstance();

            instance.Load();
            
            _csvParserMock.Verify(x => x.Parse(@"D:\Webometrics\WebsitesInfo.csv"));
        }
    }
}