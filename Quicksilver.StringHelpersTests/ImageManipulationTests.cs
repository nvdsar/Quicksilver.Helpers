using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quicksilver.PersianDate;
using Quicksilver.PictureResizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Quicksilver.PictureResizer.Tests
{
    [TestClass()]
    public class ImageManipulationTests
    {
        private string fileUploadAddress;
        private string fileResizePath;
        private string generatedFileName;
        [TestInitialize]
        public void Initialize()
        {
            fileUploadAddress = "..\\..\\..\\..\\Quicksilver.Test.MockData\\picture.jpg";
            string fileNameGuid = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            string fileNameDateTime = DateTime.Now.ToPersianDate(DateFormat.yyyyMMdd_HHmm);
            generatedFileName = $"{fileNameDateTime}-{fileNameGuid}";
            fileResizePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload");
        }
        [TestMethod()]
        public void SaveResizeAndThumbnailTest()
        {


            var extension = Path.GetExtension(fileUploadAddress).ToLowerInvariant();

            Stream stream = File.OpenRead(fileUploadAddress);

            ImageSizer.SaveResize(stream, extension, 0.75, generatedFileName, fileResizePath);
            ImageSizer.SaveThumbnail(Path.Combine(fileResizePath, $"{generatedFileName}{extension}"), 150);
        }

    }
}