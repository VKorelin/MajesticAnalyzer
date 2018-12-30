using System;
using System.Collections.Generic;
using System.IO;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.Majestic;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MajesticAnalyzer.Tests
{
    [TestFixture]
    public class UniversityLoaderServiceTests
    {
        private Mock<IBacklinksLoader> _backlinksLoaderMock;
        private Mock<IRefdomainsLoader> _refdomainsLoaderMock;

        [SetUp]
        public void SetUp()
        {
            _backlinksLoaderMock = new Mock<IBacklinksLoader>();
            _refdomainsLoaderMock = new Mock<IRefdomainsLoader>();
        }

        private UniversityLoaderService CreateInstance()
            => new UniversityLoaderService(_backlinksLoaderMock.Object, _refdomainsLoaderMock.Object);

        private static UniversityInfo CreateUniversityInfo(string url) => new UniversityInfo {Uri = new Uri(url)};

        private static DomainInfo CreateDomainInfo(string hostName) => new DomainInfo {Host = hostName};

        [Test]
        public void LoadsUniversities()
        {
            // Given
            var universityInfo = CreateUniversityInfo("http://test1/");

            var domainInfo11 = CreateDomainInfo("h1");
            const string backlink111 = "http://h1/backlink111/";
            const string backlink112 = "http://h1/backlink112/";
            
            var domainInfo12 = CreateDomainInfo("h2");
            const string backlink121 = "http://h2/backlink121/";

            _backlinksLoaderMock
                .Setup(x => x.Load(universityInfo))
                .Returns(new List<Backlink> {new Backlink(backlink111), new Backlink(backlink112), new Backlink(backlink121)});

            _refdomainsLoaderMock
                .Setup(x => x.Load(universityInfo))
                .Returns(new List<DomainInfo> {domainInfo11, domainInfo12});

            var instance = CreateInstance();

            // When
            var university = instance.Load(universityInfo);

            // Then
            university.Info.ShouldBe(universityInfo);
            university.ReffResources.Count.ShouldBe(2);

            var resource11 = university.ReffResources[0];
            resource11.Backlinks.Count.ShouldBe(2);
            CollectionAssert.AreEquivalent(
                resource11.Backlinks,
                new List<string> { backlink111, backlink112 });
            resource11.UniversityInfo.ShouldBe(universityInfo);
            resource11.Domain.ShouldBe(domainInfo11);
            
            var reffResource12 = university.ReffResources[1];
            reffResource12.Backlinks.Count.ShouldBe(1);
            CollectionAssert.AreEquivalent(
                reffResource12.Backlinks,
                new List<string> { backlink121 });
            reffResource12.UniversityInfo.ShouldBe(universityInfo);
            reffResource12.Domain.ShouldBe(domainInfo12);
        }

        [Test]
        public void ThrowsInvalidDataExceptionWhenNoReffDomainsFoundForUniversity()
        {
            var universityInfo = CreateUniversityInfo("http://test1/");
            _refdomainsLoaderMock.Setup(x => x.Load(It.IsAny<UniversityInfo>())).Returns(new List<DomainInfo>());
            var instance = CreateInstance();

            var exception = Should.Throw<InvalidDataException>(() => instance.Load(universityInfo));
            exception.Message.ShouldBe("No reff domains for http://test1/ university found");
            _refdomainsLoaderMock.Verify(x => x.Load(universityInfo), Times.Once);
            _backlinksLoaderMock.Verify(x => x.Load(It.IsAny<UniversityInfo>()), Times.Never);
        }
    }
}