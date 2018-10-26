﻿using MajesticAnalyzer.Html;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;

namespace MajesticAnalyzer.Tests.Html
{
    [TestFixture]
    public class HtmlWrapperFactoryTest
    {
        private HtmlWrapperFactory CreateInstance(Func<Uri, string, IHtmlWrapper> factory)
        {
            return new HtmlWrapperFactory(factory);
        }

        [Test]
        public void CreatesHtmlWrapper()
        {
            var wrapper = Mock.Of<IHtmlWrapper>();

            var wrapperFactory = CreateInstance((url, html) => wrapper);

            var htmlWrapper = wrapperFactory.Create(new Uri("http://www.yandex.ru"), "html");

            htmlWrapper.ShouldBeSameAs(wrapper);
        }
    }
}
