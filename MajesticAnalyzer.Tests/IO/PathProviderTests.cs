using System;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.IO;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MajesticAnalyzer.Tests.IO
{
    [TestFixture]
    public class PathProviderTests
    {
        private Mock<IConfigurationProvider> _configurationProviderMock;
        private PathProvider CreateInstance() => new PathProvider(_configurationProviderMock.Object);

        [SetUp]
        public void SetUp()
        {
            _configurationProviderMock = new Mock<IConfigurationProvider>();
        }

        [Test]
        public void ExposesHomeDirectory()
        {
            _configurationProviderMock.Setup(x => x.GetHomeDirectory()).Returns("test");
            var instance = CreateInstance();

            var homeDir = instance.HomeDirectory;
            
            homeDir.ShouldBe("test");
        }

        [Test]
        public void GetsContentOutputPath()
        {
            _configurationProviderMock.Setup(x => x.GetHomeDirectory()).Returns("test");
            var instance = CreateInstance();

            var outputPath = instance.GetContentOutputPath(new UniversityInfo {Uri = new Uri("http://www.host.ru")});
            
            outputPath.ShouldBe("test\\www.host.ru\\content.csv");
        }
    }
}