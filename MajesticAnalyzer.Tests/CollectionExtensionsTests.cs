using System;
using System.Collections.Generic;
using MajesticAnalyzer.Extensions;
using NUnit.Framework;
using Shouldly;

namespace MajesticAnalyzer.Tests
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        #region Shuffle method
        
        [Test]
        public void ThrowsArgumentExceptionOnGetMixedElementsWhenMaxCountNegative()
        {
            var exception = Should.Throw<ArgumentException>(() => (new List<object>()).Shuffle(-1));
            exception.ParamName.ShouldBe("maxCount");
            exception.Message.ShouldBe("MaxCount of elements should not be negative\r\nParameter name: maxCount");
        }

        [Test]
        public void ReturnsEmptyCollectionIfMaxIsZero()
        {
            var collection =new List<object> {new object()};

            var mixed = collection.Shuffle(0);
            
            mixed.ShouldBeEmpty();
        }

        [Test]
        public void ReturnsMaxCountOfElements()
        {
            var collection =new List<int> {12, 11, 6, 4, 8};

            var mixed = collection.Shuffle(4);
            
            mixed.Count.ShouldBe(4);
            mixed.ShouldBeSubsetOf(collection);
            mixed.ShouldBeUnique();
        }
        
        [Test]
        public void ReturnsCollectionCountElementsWhenMaxCountIsMore()
        {
            var collection =new List<int> {12, 11, 6, 4, 8};

            var mixed = collection.Shuffle(6);
            
            mixed.Count.ShouldBe(5);
            mixed.ShouldBeSubsetOf(collection);
            mixed.ShouldBeUnique();
        }
        
        #endregion
    }
}