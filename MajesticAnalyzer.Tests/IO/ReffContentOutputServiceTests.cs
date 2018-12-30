using System;
using System.Collections.Generic;
using MajesticAnalyzer.Domain;
using MajesticAnalyzer.IO;
using MajesticAnalyzer.IO.Csv;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MajesticAnalyzer.Tests.IO
{
    [TestFixture]
    public class ReffContentOutputServiceTests
    {
        private Mock<ICsvWriter<ReffResource, ReffResourceMap>> _csvWriterMock;
        private Mock<IPathProvider> _pathProviderMock;
        private ReffContentOutputService _instance;

        private ReffContentOutputService CreateInstance() => new ReffContentOutputService(_pathProviderMock.Object, _csvWriterMock.Object);

        [SetUp]
        public void SetUp()
        {
            _csvWriterMock = new Mock<ICsvWriter<ReffResource, ReffResourceMap>>();
            _pathProviderMock = new Mock<IPathProvider>();

            _instance = CreateInstance();
        }
        
        [Test]
        public void ThrowsArgumentNullExceptionWhenUniversityIsNull()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _instance.WriteContent((University)null));
            exception.ParamName.ShouldBe("university");
            
            _csvWriterMock.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<IEnumerable<ReffResource>>(), It.IsAny<bool>()), Times.Never);
            _pathProviderMock.Verify(x => x.GetContentOutputPath(It.IsAny<UniversityInfo>()), Times.Never);
        }
        
        [Test]
        public void ThrowsArgumentExceptionWhenUniversityInfoOfUniversityIsNull()
        {
            var exception = Should.Throw<ArgumentException>(() => _instance.WriteContent(new University()));
            exception.ParamName.ShouldBe("Info");
            exception.Message.ShouldContain("UniversityInfo of University should not be null\r\nParameter name: Info");
            
            _csvWriterMock.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<IEnumerable<ReffResource>>(), It.IsAny<bool>()), Times.Never);
            _pathProviderMock.Verify(x => x.GetContentOutputPath(It.IsAny<UniversityInfo>()), Times.Never);
        }
        
        [Test]
        public void ThrowsArgumentNullExceptionWhenReffResourceIsNull()
        {
            var exception = Should.Throw<ArgumentNullException>(() => _instance.WriteContent((ReffResource)null));
            exception.ParamName.ShouldBe("reffResource");
            
            _csvWriterMock.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<ReffResource>(), It.IsAny<bool>()), Times.Never);
            _pathProviderMock.Verify(x => x.GetContentOutputPath(It.IsAny<UniversityInfo>()), Times.Never);
        }
        
        [Test]
        public void ThrowsArgumentExceptionWhenUniversityInfoOfReffResourceIsNull()
        {
            var exception = Should.Throw<ArgumentException>(() => _instance.WriteContent(new ReffResource()));
            exception.ParamName.ShouldBe("UniversityInfo");
            exception.Message.ShouldContain("UniversityInfo of ReffResource should not be null\r\nParameter name: UniversityInfo");
            
            _csvWriterMock.Verify(x => x.Write(It.IsAny<string>(), It.IsAny<ReffResource>(), It.IsAny<bool>()), Times.Never);
            _pathProviderMock.Verify(x => x.GetContentOutputPath(It.IsAny<UniversityInfo>()), Times.Never);
        }

        [Test]
        public void WritesReffResourcesContent()
        {
            var university = new University
            {
                Info =  new UniversityInfo(),
                ReffResources = new List<ReffResource>()
            };

            _pathProviderMock.Setup(x => x.GetContentOutputPath(university.Info)).Returns("fileName");
            
            _instance.WriteContent(university);

            _pathProviderMock.Verify(x => x.GetContentOutputPath(university.Info), Times.Once);
            _csvWriterMock.Verify(x => x.Write("fileName", university.ReffResources, false), Times.Once);
        }
        
        [Test]
        public void WritesReffResourceContent()
        {
            var reffResource = new ReffResource {UniversityInfo = new UniversityInfo()};
            
            _pathProviderMock.Setup(x => x.GetContentOutputPath(reffResource.UniversityInfo)).Returns("fileName");
            
            _instance.WriteContent(reffResource);

            _pathProviderMock.Verify(x => x.GetContentOutputPath(reffResource.UniversityInfo), Times.Once);
            _csvWriterMock.Verify(x => x.Write("fileName", reffResource, false), Times.Once);
        }
    }
}