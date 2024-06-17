﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quicksilver.PersianDate;
using Quicksilver.Test.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicksilver.PersianDate.Tests
{
    [TestClass()]
    public class PersianDateTests
    {
        [TestMethod()]
        public void ToPersianDateTest()
        {
            var date = new DateTime(2024, 6, 17);
            var persianDate = date.ToPersianDate();
            Assert.AreEqual(PersianDateMockData.FullPersianDate, persianDate);
        }

        [TestMethod()]
        public void To2DigitYearPersianDateTest()
        {
            var date = new DateTime(2024, 6, 17);
            var persianDate = date.To2DigitYearPersianDate();
            Assert.AreEqual(PersianDateMockData.TwoDigitPersianDate, persianDate);
        }

        [TestMethod()]
        public void To6DigitPersianDateTest()
        {
            var date = new DateTime(2024, 6, 17);
            var persianDate = date.To6DigitPersianDate();
            Assert.AreEqual(PersianDateMockData.SixDigitPersianDate, persianDate);
        }

        [TestMethod()]
        public void To8DigitPersianDateTest()
        {
            var date = new DateTime(2024, 6, 17);
            var persianDate = date.To8DigitPersianDate();
            Assert.AreEqual(PersianDateMockData.EightDigitPersianDate, persianDate);
        }

        [TestMethod()]
        public void ToPersianDateTimeStringTest()
        {
            var date = new DateTime(2024, 6, 17, 12, 15, 16);
            var persianDate = date.ToPersianDateTime();
            Assert.AreEqual(PersianDateMockData.FullPersianDateTime, persianDate);
        }

        [TestMethod()]
        public void PersianShortDateTimeStringTest()
        {
            var date = new DateTime(2024, 6, 17, 12, 15, 16);
            var persianDate = date.ToPersianShortDateTime();
            Assert.AreEqual(PersianDateMockData.PersianShortDateTime, persianDate);
        }

        [TestMethod()]
        public void ToGregorianDateTest()
        {
            var persianDate = "date is 1403/03/28 hh 22 Navid";
            var date = persianDate.ToGregorianDate();
            Assert.AreEqual(PersianDateMockData.GregorianDate, date);
        }

        [TestMethod()]
        public void ShortTimeStringTest()
        {
            var date = new DateTime(2024, 6, 17, 12, 15, 16);
            var persianDate = date.ShortTimeString();
            Assert.AreEqual(PersianDateMockData.ShortTime, persianDate);
        }

        [TestMethod()]
        public void ShortDateStringTest()
        {
            var date = new DateTime(2024, 6, 17, 12, 15, 16);
            var persianDate = date.ShortDateString();
            Assert.AreEqual(PersianDateMockData.ShortDate, persianDate);
        }



        [TestMethod()]
        public void ShortDateString8Test()
        {
            var date = new DateTime(2024, 6, 17, 12, 15, 16);
            var persianDate = date.ShortDateString8();
            Assert.AreEqual(PersianDateMockData.Short8Date, persianDate);
        }


        [TestMethod()]
        public void GetFirstDayOfSolarHijriTest()
        {
            var firstDay = PersianDate.GetFirstDayOfSolarHijri(1403);
            Assert.AreEqual(PersianDateMockData.FirstDayOfYear, firstDay);
        }

        [TestMethod()]
        public void GetDayOfWeekTest()
        {
            var Monday = PersianDate.GetDayOfWeek(2);
            Assert.AreEqual(PersianDateMockData.Monday, Monday);
        }

        [TestMethod()]
        public void GetDayOfWeekTest1()
        {
            var Monday = PersianDate.GetDayOfWeek(DayOfWeek.Monday);
            Assert.AreEqual(PersianDateMockData.Monday, Monday);
        }

        [TestMethod()]
        public void GetMonthNameTest()
        {
            var khordad = PersianDate.GetMonthName(3);
            Assert.AreEqual(PersianDateMockData.Khordad, khordad);
        }
    }
}