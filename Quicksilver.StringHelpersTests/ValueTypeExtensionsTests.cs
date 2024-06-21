using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quicksilver.DataConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicksilver.DataConversion.Tests
{
    [TestClass()]
    public class ValueTypeExtensionsTests
    {
        public void ToNullableDecimalTest()
        {
            var d = "2.34";
            decimal? d1 = null;
            var r = d.ToNullableDecimal();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(decimal));
            var r1 = d1.ToNullableDecimal();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ToNullableDoubleTest()
        {
            var d = "2.34";
            double? d1 = null;
            var r = d.ToNullableDouble();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(double));
            var r1 = d1.ToNullableDouble();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ToNullableBooleanTest()
        {
            var d = "true";
            bool? d1 = null;
            var r = d.ToNullableBoolean();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(bool));
            Assert.IsTrue(r);
            var r1 = d1.ToNullableBoolean();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ToNullableByteTest()
        {
            var d = "2";
            byte? d1 = null;
            var r = d.ToNullableByte();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(byte));
            var r1 = d1.ToNullableByte();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ToNullableInt16Test()
        {
            var d = "2";
            short? d1 = null;
            var r = d.ToNullableInt16();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(short));
            var r1 = d1.ToNullableInt16();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ToNullableInt32Test()
        {
            var d = "2";
            int? d1 = null;
            var r = d.ToNullableInt32();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(int));
            var r1 = d1.ToNullableInt32();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ToNullableInt64Test()
        {
            var d = "2";
            long? d1 = null;
            var r = d.ToNullableInt64();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(long));
            var r1 = d1.ToNullableInt64();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ConvertToByteArrayTest()
        {

        }

        [TestMethod()]
        public void ToByteArrayTest()
        {

        }

        [TestMethod()]
        public void ToGuidTest()
        {
            var d = Guid.NewGuid().ToString();
            Guid? d1 = null;
            var r = d.ToGuid();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(Guid));
            var r1 = d1.ToGuid();
            Assert.AreEqual(Guid.Empty, r1);
        }

        [TestMethod()]
        public void ToNullableGuidTest()
        {
            var d = Guid.NewGuid().ToString();
            Guid? d1 = null;
            var r = d.ToNullableGuid();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(Guid));
            var r1 = d1.ToNullableGuid();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ToNullableStringTest()
        {
            var d = 223;
            object? d1 = null;
            var r = d.ToNullableString();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(string));
            var r1 = d1.ToNullableString();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            var d = 223;
            const string str = "test";
            object? d1 = null;
            var r = d.ToString(str);
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(string));
            var r1 = d1.ToString(str);
            Assert.AreEqual(str, r1, false);
        }

        [TestMethod()]
        public void ToNullableDateTimeTest()
        {
            var d = DateTime.Now;
            DateTime? d1 = null;
            var r = d.ToNullableDateTime();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(DateTime));
            var r1 = d1.ToNullableDateTime();
            Assert.IsNull(r1);
        }

        [TestMethod()]
        public void ToDecimalTest()
        {
            var d = "2.34";
            decimal? d1 = null;
            var r = d.ToDecimal((decimal)0.34);
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(decimal));
            var r1 = d1.ToDecimal((decimal)0.34);
            Assert.AreEqual((decimal)0.34, r1);
        }

        [TestMethod()]
        public void ToDoubleTest()
        {
            var d = "2.34";
            double? d1 = null;
            var r = d.ToDouble(0.25);
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(double));
            var r1 = d1.ToDouble(0.25);
            Assert.AreEqual(0.25, r1);
        }

        [TestMethod()]
        public void ToInt32Test()
        {
            var d = "2";
            int? d1 = null;
            var r = d.ToInt32();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(int));
            var r1 = d1.ToInt32(1);
            Assert.AreEqual(1, r1);
        }

        [TestMethod()]
        public void ToInt64Test()
        {
            var d = "2";
            long? d1 = null;
            var r = d.ToInt64();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(long));
            var r1 = d1.ToInt64(1000);
            Assert.AreEqual(1000, r1);
        }

        [TestMethod()]
        public void ToInt16Test()
        {
            var d = "2";
            short? d1 = null;
            var r = d.ToInt16();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(short));
            var r1 = d1.ToInt16(24);
            Assert.AreEqual(24, r1);
        }

        [TestMethod()]
        public void ToByteTest()
        {
            var d = "2";
            byte? d1 = null;
            var r = d.ToByte();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(byte));
            var r1 = d1.ToInt16(2);
            Assert.AreEqual(2, r1);
        }

        [TestMethod()]
        public void ToBooleanTest()
        {
            var d = "true";
            bool? d1 = null;
            var r = d.ToBoolean();
            Assert.IsNotNull(r);
            Assert.IsInstanceOfType(r, typeof(bool));
            Assert.IsTrue(r);
            var r1 = d1.ToBoolean();
            Assert.IsFalse(r1);
        }

        [TestMethod()]
        public void ToDateTimeTest()
        {

        }

        [TestMethod()]
        public void ToDateTimeTest1()
        {

        }
    }
}