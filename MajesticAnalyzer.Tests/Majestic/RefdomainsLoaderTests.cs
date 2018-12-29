using System;
using System.Collections.Generic;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.IO.Csv;
using MajesticAnalyzer.Majestic;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MajesticAnalyzer.Tests.Majestic
{
    [TestFixture]
    public class RefdomainsLoaderTests
    {
        private Mock<ICsvParser<DomainInfo, RefdomainsMap>> _csvParserMock;
        
        private RefdomainsLoader CreateInstance() 
            => new RefdomainsLoader(_csvParserMock.Object, Mock.Of<IPathProvider>(x => x.HomeDirectory == string.Empty));

        [SetUp]
        public void SetUp()
        {
            _csvParserMock = new Mock<ICsvParser<DomainInfo, RefdomainsMap>>();
        }

        [Test]
        public void LoadsRefdomainsForUniversityInfo()
        {
            var instance = CreateInstance();
            var expected = new List<DomainInfo>();
            _csvParserMock.Setup(x => x.Parse(It.IsAny<string>())).Returns(expected);
            var universityInfo = new UniversityInfo
            {
                Uri = new Uri("http://test.com/")
            };

            var actual = instance.Load(universityInfo);
            
            actual.ShouldBe(expected);
        }

        [Test]
        public void ThrowsArgumentNullExceptionIfUniversityInfoNull()
        {
            var instance = CreateInstance();

            var exception = Should.Throw<ArgumentNullException>(() => instance.Load(null));
            exception.ParamName.ShouldBe("universityInfo");
        }
    }
}