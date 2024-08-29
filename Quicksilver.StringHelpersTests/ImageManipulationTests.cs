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
            //fileUploadAddress = "..\\..\\..\\..\\Quicksilver.Test.MockData\\picture.jpg";
            fileUploadAddress = "C:\\Navid\\Test\\logo.png";
            string fileNameGuid = Guid.NewGuid().ToString().Replace("-", "").ToLower();
            string fileNameDateTime = DateTime.Now.ToPersianDate(DateFormat.yyyyMMdd_HHmm);
            generatedFileName = $"{fileNameDateTime}-{fileNameGuid}";
            fileResizePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "upload");
        }

        [TestMethod()]
        public void SaveThumbnailTest()
        {
            ImageSizer.SaveThumbnail(fileUploadAddress, 150);
        }

        [TestMethod()]
        public void ResizeTest()
        {
            var extension = Path.GetExtension(fileUploadAddress).ToLowerInvariant();

            var imageData = Convert.ToBase64String(File.ReadAllBytes(fileUploadAddress));
            var imageResizeFactors = new ImageResizeFactors()
            {
                Extension = extension,
                ScaleFactor = 0.6,
                Quality = SkiaSharp.SKFilterQuality.Low,
            };
            var resizedBase64 = ImageSizer.Resize(imageData, imageResizeFactors);
            Assert.IsNotNull(resizedBase64);
        }

        [TestMethod()]
        public void ThumbnailTest()
        {
            var extension = Path.GetExtension(fileUploadAddress).ToLowerInvariant();

            var imageData = Convert.ToBase64String(File.ReadAllBytes(fileUploadAddress));
            var resizedBase64 = ImageSizer.Thumbnail(imageData, extension, 150);
            Assert.IsNotNull(resizedBase64);
        }

        [TestMethod()]
        public void SaveResizeTest()
        {
                        var extension = Path.GetExtension(fileUploadAddress).ToLowerInvariant();

            Stream stream = File.OpenRead(fileUploadAddress);
            var imageResizeFactors = new ImageResizeFactors()
            {
                Extension = extension,
                ScaleFactor = 0.6,
                Quality = SkiaSharp.SKFilterQuality.High,
            };
            ImageSizer.SaveResize(stream, imageResizeFactors, generatedFileName, fileResizePath);
        }
    }
}