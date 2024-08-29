using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicksilver.PictureResizer
{
    /// <summary>
    /// Represents a watermark to be applied to an image.
    /// </summary>
    public record Watermark
    {
        /// <summary>
        /// Gets or sets the watermark image data as a byte array.
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// Gets or sets the watermark image data as a base64-encoded string.
        /// </summary>
        public string Base64Img { get; set; }

        /// <summary>
        /// Gets or sets whether to reverse the watermark horizontally.
        /// </summary>
        public bool ReverseX { get; set; } = true;

        /// <summary>
        /// Gets or sets whether to reverse the watermark vertically.
        /// </summary>
        public bool ReverseY { get; set; } = true;

        /// <summary>
        /// Gets or sets the position of the watermark relative to the top-left corner of the image.
        /// </summary>
        public Point Position { get; set; }
    }
    /// <summary>
    /// Provides methods for adding watermarks to images.
    /// </summary>
    public class WatermarkAdapter
    {
        /// <summary>
        /// Adds one or more watermarks to an image represented as a byte array.
        /// </summary>
        /// <param name="image">The image data as a byte array.</param>
        /// <param name="watermarks">An array of watermark objects to apply to the image.</param>
        /// <returns>A byte array containing the image with the added watermarks.</returns>
        public byte[] AddWatermark(byte[] image, params Watermark[] watermarks)
        {
            using (MemoryStream ms = new MemoryStream(image))
            using (SKBitmap skImage = SKBitmap.Decode(ms))
            using (SKCanvas canvas = new SKCanvas(skImage))
            {
                foreach (Watermark watermark in watermarks)
                {
                    using (SKBitmap w = SKBitmap.Decode(watermark.Image))
                    {
                        int x = watermark.ReverseX ? skImage.Width - w.Width - watermark.Position.X : watermark.Position.X;
                        int y = watermark.ReverseY ? skImage.Height - w.Height - watermark.Position.Y : watermark.Position.Y;

                        canvas.DrawBitmap(w, new SKPoint(x, y));
                    }
                }

                using (SKData data = skImage.Encode(SKEncodedImageFormat.Png, 100))
                {
                    return data.ToArray();
                }
            }
        }
        /// <summary>
        /// Adds one or more watermarks to a base64-encoded image string and returns the result as a base64-encoded string.
        /// </summary>
        /// <param name="imageBase64">The base64-encoded image data.</param>
        /// <param name="watermarks">An array of watermark objects to apply to the image.</param>
        /// <returns>A base64-encoded string containing the image with the added watermarks.</returns>
        public string AddWatermark(string imageBase64, params Watermark[] watermarks)
        {
            var imgData = Convert.FromBase64String(imageBase64);
            return Convert.ToBase64String(AddWatermark(imgData, watermarks));
        }
    }
}
