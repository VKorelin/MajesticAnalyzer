using System;
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
    public class BacklinksLoaderTests
    {
        private Mock<ICsvParser<Backlink, BacklinksMap>> _csvParserMock;
        
        private BacklinksLoader CreateInstance() 
            => new BacklinksLoader(_csvParserMock.Object, Mock.Of<IPathProvider>(x => x.HomeDirectory == string.Empty));

        [SetUp]
        public void SetUp()
        {
            _csvParserMock = new Mock<ICsvParser<Backlink, BacklinksMap>>();
        }

        [Test]
        public void LoadsRefdomainsForUniversityInfo()
        {
            var instance = CreateInstance();
            var expected = new List<Backlink>();
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