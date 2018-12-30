using System;
using System.Collections.Generic;
using System.IO;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.Majestic;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MajesticAnalyzer.Tests
{
    [TestFixture]
    public class UniversitiesLoaderServiceTests
    {
        private Mock<IWebsitesInfoLoader> _websitesInfoLoaderMock;
        private Mock<IPathProvider> _pathProviderMock;
        private Mock<IUniversityLoaderService> _universityLoaderServiceMock;

        [SetUp]
        public void SetUp()
        {
            _websitesInfoLoaderMock = new Mock<IWebsitesInfoLoader>();
            _pathProviderMock = new Mock<IPathProvider>();
            _universityLoaderServiceMock = new Mock<IUniversityLoaderService>();
        }

        private UniversitiesLoaderService CreateInstance()
            => new UniversitiesLoaderService(
                _websitesInfoLoaderMock.Object,
                _pathProviderMock.Object,
                _universityLoaderServiceMock.Object);

        private static UniversityInfo CreateUniversityInfo(string url) => new UniversityInfo {Uri = new Uri(url)};

        private static DomainInfo CreateDomainInfo(string hostName) => new DomainInfo {Host = hostName};

        [Test]
        public void LoadsUniversities()
        {
            // Given
            var universityInfo1 = CreateUniversityInfo("http://test1/");
            var universityInfo2 = CreateUniversityInfo("http://test2/");
            var universityInfos = new List<UniversityInfo> {universityInfo1, universityInfo2};

            _websitesInfoLoaderMock.Setup(x => x.Load()).Returns(universityInfos);
            _pathProviderMock.SetupGet(x => x.ChildDirectories).Returns(new List<string> {"test1", "test2"});

            var instance = CreateInstance();

            // When
            instance.LoadUniversities();

            // Then
            _universityLoaderServiceMock.Verify(x => x.Load(universityInfo1), Times.Once);
            _universityLoaderServiceMock.Verify(x => x.Load(universityInfo2), Times.Once);
        }

        [Test]
        public void DoesNotLoadUniversityIfNoUniversityFolderExists()
        {
            var universityInfo1 = CreateUniversityInfo("http://test1/");

            _websitesInfoLoaderMock.Setup(x => x.Load()).Returns(new List<UniversityInfo> {universityInfo1});
            _pathProviderMock.SetupGet(x => x.ChildDirectories).Returns(new List<string>());

            var instance = CreateInstance();

            instance.LoadUniversities();
            
            _universityLoaderServiceMock.Verify(x => x.Load(universityInfo1), Times.Never);
        }

        [Test]
        public void LoadUniversity()
        {
            const string universityHost = "www.host.com";
            _pathProviderMock.SetupGet(x => x.ChildDirectories).Returns(new List<string> {"www.host.com"});
            var universityInfo = CreateUniversityInfo("http://www.host.com/");
            _websitesInfoLoaderMock.Setup(x => x.Load()).Returns(new List<UniversityInfo> {universityInfo});

            var instance = CreateInstance();

            instance.LoadUniversity(universityHost);
            
            _universityLoaderServiceMock.Verify(x => x.Load(universityInfo), Times.Once);
        }

        [Test]
        public void ThrowsDirectoryNotFoundExceptionWhenUniversityFolderNotFound()
        {
            const string universityHost = "www.host.com";
            _pathProviderMock.SetupGet(x => x.ChildDirectories).Returns(new List<string> {"test"});

            var instance = CreateInstance();

            var exception = Should.Throw<DirectoryNotFoundException>(() => instance.LoadUniversity(universityHost));
            exception.Message.ShouldBe("Cannot find www.host.com university folder");
            _universityLoaderServiceMock.Verify(x => x.Load(It.IsAny<UniversityInfo>()), Times.Never);
        }
    }
}