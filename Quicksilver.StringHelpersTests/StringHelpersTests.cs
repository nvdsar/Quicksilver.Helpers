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
    public class StringHelpersTests
    {
        [TestMethod()]
        public void GetPluralCommaTest()
        {
            var texts = new string[] { "سبب", "گلابی", "توت فرنگی", "هلو", "شلیل" };
            var result = StringHelpers.GetPluralComma(string.Join(',', texts));
            var mockData = StringHelpersMockData.PluralComma;
            Assert.AreEqual(mockData, result, true);
        }

        [TestMethod()]
        public void GetPluralCommaTest1()
        {
            var texts = new string[] { "سبب", "گلابی", "توت فرنگی", "هلو", "شلیل" };
            var result = StringHelpers.GetPluralComma(texts.ToList());
            var mockData = StringHelpersMockData.PluralComma;
            Assert.AreEqual(mockData, result, true);
        }
    }
}