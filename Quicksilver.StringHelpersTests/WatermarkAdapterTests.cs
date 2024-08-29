using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quicksilver.PictureResizer;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Quicksilver.PictureResizer.Tests
{
    [TestClass()]
    public class WatermarkAdapterTests
    {
        [TestMethod()]
        public void AddWatermarkTest()
        {
            var img = File.ReadAllBytes("C:\\Navid\\Test\\c2.png");
            var logo = File.ReadAllBytes("C:\\Navid\\Test\\Thumbnail\\logo.png");
            var correct = File.ReadAllBytes("C:\\Navid\\Test\\Thumbnail\\yes.png");
            var error = File.ReadAllBytes("C:\\Navid\\Test\\Thumbnail\\error.png");
            //var logoresize = ImageSizer.SaveResize()
            Watermark[] watermark = [
                new Watermark(){Image = error,Position = new System.Drawing.Point(10, 10),ReverseX = true,ReverseY = true,},
                new Watermark(){Image = logo,Position = new System.Drawing.Point(10, 10),ReverseX = true,ReverseY = false}
            ];
            var image = new WatermarkAdapter().AddWatermark(img, watermark);
            var base64 = Convert.ToBase64String(image);

            File.WriteAllBytes("C:\\Navid\\Test\\c1_waterMarked.png", image);
        }
    }
}