using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quicksilver.DataConversion;
using Quicksilver.EnumerableHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Quicksilver.Test.MockData;

namespace Quicksilver.EnumerableHelpers.Tests
{
    [TestClass()]
    public class DynamicListExtensionsTests
    {
        private List<TestObject> list;

        [TestInitialize]
        public void Initialize()
        {
            list =
            [
                new TestObject() { Id = 1, IsValid = true, Name ="FirstObject" },
                new TestObject() { Id = 2, IsValid = false, Name ="SecondObject" },
                new TestObject() { Id = 3, IsValid = true, Name ="ThirdObject" },
            ];
        }

        [TestMethod()]
        public void ToDataTableTest()
        {
            var result = list.ToDataTable();
            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            Assert.IsInstanceOfType(result, typeof(DataTable));
            Assert.IsTrue(result.Rows.Count == 3);
            Assert.IsTrue(result.Columns.Count == 3);
            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void ToDynamicListTest()
        {
            var result = list.ToDataTable().ToDynamicList();
            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            Assert.AreEqual(jsonResult, EnumerableHelpersMockData.fullJsonObject);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ToDynamicListTest1()
        {
            var columns = new List<string>() { "Name", "IsValid" };
            var result = list.ToDataTable().ToDynamicList(columns);
            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            Assert.AreEqual(jsonResult, EnumerableHelpersMockData.jsonObject);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ToDynamicListTest2()
        {
            var result = list.ToDynamicList();
            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            Assert.AreEqual(jsonResult, EnumerableHelpersMockData.fullJsonObject);
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void ToDynamicListTest3()
        {
            var columns = new List<string>() { "Name", "IsValid" };
            var result = list.ToDynamicList(columns);
            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            Assert.AreEqual(jsonResult, EnumerableHelpersMockData.jsonObject);
            Assert.IsNotNull(result);
        }
        private class TestObject
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsValid { get; set; }
        }
    }

}