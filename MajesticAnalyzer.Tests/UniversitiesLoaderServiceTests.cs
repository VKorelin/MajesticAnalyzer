using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private Mock<IBacklinksLoader> _backlinksLoaderMock;
        private Mock<IRefdomainsLoader> _refdomainsLoaderMock;
        private Mock<IPathProvider> _pathProviderMock;

        [SetUp]
        public void SetUp()
        {
            _websitesInfoLoaderMock = new Mock<IWebsitesInfoLoader>();
            _backlinksLoaderMock = new Mock<IBacklinksLoader>();
            _refdomainsLoaderMock = new Mock<IRefdomainsLoader>();
            _pathProviderMock = new Mock<IPathProvider>();
        }

        private UniversitiesLoaderService CreateInstance()
            => new UniversitiesLoaderService(
                _websitesInfoLoaderMock.Object,
                _backlinksLoaderMock.Object,
                _refdomainsLoaderMock.Object,
                _pathProviderMock.Object);

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

            var domainInfo11 = CreateDomainInfo("h1");
            const string backlink111 = "http://h1/backlink111/";
            const string backlink112 = "http://h1/backlink112/";
            
            var domainInfo12 = CreateDomainInfo("h2");
            const string backlink121 = "http://h2/backlink121/";
            
            var domainInfo21 = CreateDomainInfo("h3");
            const string backlink211 = "http://h3/backlink211/";

            _backlinksLoaderMock
                .Setup(x => x.Load(universityInfo1))
                .Returns(new List<Backlink> {new Backlink(backlink111), new Backlink(backlink112), new Backlink(backlink121)});

            _backlinksLoaderMock
                .Setup(x => x.Load(universityInfo2))
                .Returns(new List<Backlink> {new Backlink(backlink211)});

            _refdomainsLoaderMock
                .Setup(x => x.Load(universityInfo1))
                .Returns(new List<DomainInfo> {domainInfo11, domainInfo12});

            _refdomainsLoaderMock
                .Setup(x => x.Load(universityInfo2))
                .Returns(new List<DomainInfo> {domainInfo21});

            var instance = CreateInstance();

            // When
            var universities = instance.LoadUniversities();

            // Then
            universities.Count.ShouldBe(2);
            
            // First university
            var firstUniversity = universities[0];
            firstUniversity.Info.ShouldBe(universityInfo1);
            firstUniversity.ReffResources.Count.ShouldBe(2);

            var resource11 = firstUniversity.ReffResources[0];
            resource11.Backlinks.Count.ShouldBe(2);
            CollectionAssert.AreEquivalent(
                resource11.Backlinks,
                new List<string> { backlink111, backlink112 });
            resource11.UniversityInfo.ShouldBe(universityInfo1);
            resource11.Domain.ShouldBe(domainInfo11);
            
            var reffResource12 = firstUniversity.ReffResources[1];
            reffResource12.Backlinks.Count.ShouldBe(1);
            CollectionAssert.AreEquivalent(
                reffResource12.Backlinks,
                new List<string> { backlink121 });
            reffResource12.UniversityInfo.ShouldBe(universityInfo1);
            reffResource12.Domain.ShouldBe(domainInfo12);
            
            // Second university
            var secondUniversity = universities[1];
            secondUniversity.Info.ShouldBe(universityInfo2);
            secondUniversity.ReffResources.Count.ShouldBe(1);
            
            var resource21 = secondUniversity.ReffResources[0];
            resource21.Backlinks.Count.ShouldBe(1);
            CollectionAssert.AreEquivalent(
                resource21.Backlinks,
                new List<string> { backlink211 });
            resource21.UniversityInfo.ShouldBe(universityInfo2);
            resource21.Domain.ShouldBe(domainInfo21);
        }

        [Test]
        public void DoesNotLoadUniversityIfNoUniversityFolderExists()
        {
            var universityInfo1 = CreateUniversityInfo("http://test1/");

            _websitesInfoLoaderMock.Setup(x => x.Load()).Returns(new List<UniversityInfo> {universityInfo1});
            _pathProviderMock.SetupGet(x => x.ChildDirectories).Returns(new List<string>());

            var instance = CreateInstance();

            instance.LoadUniversities().ShouldBeEmpty();
            _refdomainsLoaderMock.Verify(x => x.Load(It.IsAny<UniversityInfo>()), Times.Never);
            _backlinksLoaderMock.Verify(x => x.Load(It.IsAny<UniversityInfo>()), Times.Never);
        }

        [Test]
        public void ThrowsInvalidDataExceptionWhenNoReffDomainsFoundForUniversity()
        {
            var universityInfo1 = CreateUniversityInfo("http://test1/");
            _websitesInfoLoaderMock.Setup(x => x.Load()).Returns(new List<UniversityInfo> {universityInfo1});

            _pathProviderMock.SetupGet(x => x.ChildDirectories).Returns(new List<string> {"test1"});
            _refdomainsLoaderMock.Setup(x => x.Load(It.IsAny<UniversityInfo>())).Returns(new List<DomainInfo>());

            var instance = CreateInstance();

            var exception = Should.Throw<InvalidDataException>(() => instance.LoadUniversities());
            exception.Message.ShouldBe("No reff domains for http://test1/ university found");
            _refdomainsLoaderMock.Verify(x => x.Load(universityInfo1), Times.Once);
            _backlinksLoaderMock.Verify(x => x.Load(It.IsAny<UniversityInfo>()), Times.Never);
        }
    }
}