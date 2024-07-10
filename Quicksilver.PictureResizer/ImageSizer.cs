using Quicksilver.DataConversion;
using SkiaSharp;


namespace Quicksilver.PictureResizer
{
    public class ImageSizer
    {
        private const string ThumbnailFolderName = "Thumbnail";
        private static int minAcceptableSize = 512 * 1000;

        /// <summary>
        /// Change the minimum acceptable size of resized image.
        /// </summary>
        /// <param name="size">Acceptable size, based on bytes</param>
        public static void ChangeMinAcceptableFileSize(int size)
        {
            minAcceptableSize = size;
        }
        /// <summary>
        /// Resize an image stream into scale factor and return image stream
        /// </summary>
        /// <param name="imgStream">stream of image to resize</param>
        /// <param name="extension">extension of image</param>
        /// <param name="scaleFactor">scale factor between 0 - 1 </param>
        /// <returns>Resized image stream</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Stream Resize(Stream imgStream, string extension, double scaleFactor)
        {
            if (scaleFactor <= 0.0)
            {
                throw new ArgumentException("Negative scale factor is not allowed", nameof(scaleFactor));
            }

            if (scaleFactor == 1.0)
            {
                return imgStream;
            }

            using (var skImage = SKBitmap.Decode(imgStream))
            {
                int newHeight = (int)(skImage.Height * scaleFactor);
                int newWidth = (int)(skImage.Width * scaleFactor);

                using (var scaledBitmap = skImage.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.Low))
                {
                    using (var image = SKImage.FromBitmap(scaledBitmap))
                    {
                        int qualityFactor = scaleFactor < 1 ? (scaleFactor * 100).ToInt32() : 50;
                        using (var encodedImage = image.Encode(MimeTypeMapping.GetSkiaSharpImageFormatFromExtension(extension), qualityFactor))
                        {
                            if (encodedImage.Size < minAcceptableSize)
                            {
                                var stream = new MemoryStream();
                                encodedImage.SaveTo(stream);
                                stream.Seek(0, SeekOrigin.Begin);
                                return stream;
                            }

                        }
                    }
                }
            }
            return Resize(imgStream, extension, scaleFactor * 0.75);
        }
        /// <summary>
        /// Create thumbnail of an image
        /// </summary>
        /// <param name="imgStream">stream of image to resize</param>
        /// <param name="extension">extension of image</param>
        /// <param name="thumbnailHeight">thumbnail height in pixels</param>
        /// <returns>Thumbnail image stream</returns>
        public static Stream Thumbnail(Stream imgStream, string extension, int thumbnailHeight = 150)
        {
            using (var skImage = SKBitmap.Decode(imgStream))
            {
                var ratio = (double)thumbnailHeight / (double)skImage.Height;
                int newHeight = (int)(skImage.Height * ratio);
                int newWidth = (int)(skImage.Width * ratio);

                using (var scaledBitmap = skImage.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.Low))
                {
                    using (var image = SKImage.FromBitmap(scaledBitmap))
                    {
                        using (SKData encodedImage = image.Encode(MimeTypeMapping.GetSkiaSharpImageFormatFromExtension(extension), 50))
                        {
                            var stream = new MemoryStream();
                            encodedImage.SaveTo(stream);
                            stream.Seek(0, SeekOrigin.Begin);
                            return stream;

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Resize an image stream into scale factor and save on a certain address and name
        /// </summary>
        /// <param name="imgStream">stream of image to resize</param>
        /// <param name="extension">extension of image</param>
        /// <param name="scaleFactor">scale factor between 0 - 1 </param>
        /// <param name="fileName">name of the file <b>NOT</b> containing extension</param>
        /// <param name="address">Path to save the file</param>
        /// <exception cref="ArgumentException"></exception>
        public static void SaveResize(Stream imgStream, string extension, double scaleFactor, string fileName, string address)
        {

            if (scaleFactor <= 0.0)
            {
                throw new ArgumentException("Negative scale factor is not allowed", nameof(scaleFactor));
            }
            if (scaleFactor >= 1.0)
            {
                throw new ArgumentException("More than 1 scale factor is not allowed", nameof(scaleFactor));
            }


            if (Directory.Exists(address) == false)
                Directory.CreateDirectory(address);


            using (SKBitmap skImage = SKBitmap.Decode(imgStream))
            {
                int newHeight = (int)(skImage.Height * scaleFactor);
                int newWidth = (int)(skImage.Width * scaleFactor);

                using (var scaledBitmap = skImage.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.Low))
                {
                    resize(scaledBitmap, scaleFactor);
                }

                void resize(SKBitmap sKBitmap, double scaleFactor)
                {
                    using (var image = SKImage.FromBitmap(sKBitmap))
                    {
                        int qualityFactor = scaleFactor < 1 ? (scaleFactor * 100).ToInt32() : 50;
                        using (var encodedImage = image.Encode(MimeTypeMapping.GetSkiaSharpImageFormatFromExtension(extension), qualityFactor))
                        {
                            if (encodedImage.Size < minAcceptableSize)
                            {
                                using (var stream = File.OpenWrite(Path.Combine(address, $"{fileName}{extension}")))
                                {
                                    encodedImage.SaveTo(stream);
                                }
                                return;
                            }

                        }
                    }
                    resize(sKBitmap, scaleFactor * 0.75);
                }
            }


        }
        /// <summary>
        /// Create thumbnail of an image and save on "Thumbnail" folder in the file address
        /// </summary>
        /// <param name="fileAddress">full name and address of the file to create thumbnail</param>
        /// <param name="thumbnailHeight">thumbnail height in pixels</param>
        public static void SaveThumbnail(string fileAddress, int thumbPrintHeight = 150)
        {
            var fileinfo = new FileInfo(fileAddress);
            var directory = Path.Combine(fileinfo.DirectoryName, ThumbnailFolderName);
            if (Directory.Exists(directory) == false)
                Directory.CreateDirectory(directory);

            var bitmap = SKBitmap.Decode(fileAddress);
            var resizeFactor = bitmap.Height > thumbPrintHeight ? (double)thumbPrintHeight / (double)bitmap.Height : 0.5;
            int width = (int)Math.Round(bitmap.Width * resizeFactor);
            int height = (int)Math.Round(bitmap.Height * resizeFactor);
            var toBitmap = new SKBitmap(width, height, bitmap.ColorType, bitmap.AlphaType);

            var canvas = new SKCanvas(toBitmap);
            // Draw a bitmap rescaled
            canvas.SetMatrix(SKMatrix.CreateScale((float)resizeFactor, (float)resizeFactor));
            canvas.DrawBitmap(bitmap, 0, 0);
            canvas.ResetMatrix();

            var image = SKImage.FromBitmap(toBitmap);
            var data = image.Encode(MimeTypeMapping.GetSkiaSharpImageFormatFromExtension(fileinfo.Extension), 90);

            using (var stream = new FileStream(Path.Combine(directory, fileinfo.Name), FileMode.Create, FileAccess.Write))
                data.SaveTo(stream);

            data.Dispose();
            image.Dispose();
            canvas.Dispose();
            toBitmap.Dispose();
            bitmap.Dispose();
        }
    }
    internal static class MimeTypeMapping
    {
        public static string GetMimeTypeFromExtension(string extension)
        {
            ArgumentException.ThrowIfNullOrEmpty(extension, nameof(extension));

            return _mimeTypeMapping.TryGetValue(extension, out string mimeType) ? mimeType : DEFAULT_MIME_TYPE;
        }

        public static SKEncodedImageFormat GetSkiaSharpImageFormatFromExtension(string extension)
        {
            ArgumentException.ThrowIfNullOrEmpty(extension, nameof(extension));

            return _skiaSharpImageFormatMapping.TryGetValue(extension, out SKEncodedImageFormat imageFormat) ? imageFormat : DEFAULT_IMAGE_FORMAT;
        }

        private const string DEFAULT_MIME_TYPE = "application/octet-stream";
        private const SKEncodedImageFormat DEFAULT_IMAGE_FORMAT = SKEncodedImageFormat.Png;

        private static readonly Dictionary<string, string> _mimeTypeMapping = new(StringComparer.InvariantCultureIgnoreCase)
    {
        {".jpe", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".jpg", "image/jpeg"},
        {".png", "image/png"}
    };

        private static readonly Dictionary<string, SKEncodedImageFormat> _skiaSharpImageFormatMapping = new(StringComparer.InvariantCultureIgnoreCase)
    {
        {".png", SKEncodedImageFormat.Png },
        {".jpg", SKEncodedImageFormat.Jpeg },
        {".jpeg", SKEncodedImageFormat.Jpeg },
        {".jpe", SKEncodedImageFormat.Jpeg }
    };
    }
}
