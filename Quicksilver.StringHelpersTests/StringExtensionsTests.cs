using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quicksilver.StringHelpers;
using Quicksilver.Test.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicksilver.StringHelpers.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        [TestMethod()]
        public void ToPadStringTest()
        {
            var result = 1.ToPadString(5, '0');
            var mockData = StringHelpersMockData.ToPadString;
            Assert.AreEqual(mockData, result);
        }

        [TestMethod()]
        public void SubstringTrimTest()
        {
            var str = "Navid    Sargheiny";
            var result = str.SubstringTrim(0, 9);
            Assert.AreEqual(StringHelpersMockData.SubstringTrim, result);
        }

        [TestMethod()]
        public void ToCamelCaseTest()
        {
            var str = "NavidSargheiny";
            var result = str.ToCamelCase();
            Assert.AreEqual(StringHelpersMockData.CamelCase, result);
        }

        [TestMethod()]
        public void RemoveQuotationTest()
        {
            var str = "My name is \"Navid\" 'Sargheiny'";
            var result = str.RemoveQuotation();
            Assert.AreEqual(StringHelpersMockData.Sentence, result);
        }

        [TestMethod()]
        public void ReplaceWithEmptyTest()
        {
            var str = "My name11111 is \"Navid\" 'Sargheiny'";
            var result = str.ReplaceWithEmpty("\"", "'", "1");
            Assert.AreEqual(StringHelpersMockData.Sentence, result);
        }

        [TestMethod()]
        public void ReplaceWhileTest()
        {
            var str = "My        name    is         Navid          Sargheiny";
            var result = str.ReplaceWhile("  ", " ");
            Assert.AreEqual(StringHelpersMockData.Sentence, result);
        }

        [TestMethod()]
        public void SpaceTrimTest()
        {
            var str = "My        name    is         Navid          Sargheiny";
            var result = str.SpaceTrim();
            Assert.AreEqual(StringHelpersMockData.Sentence, result);
        }

        [TestMethod()]
        public void TrimEndTest()
        {
            var str = "My name is Navid Sargheiny and this is my test";
            var result = str.TrimEnd("Sargheiny");
            Assert.AreEqual(StringHelpersMockData.Sentence, result);
        }
    }
}