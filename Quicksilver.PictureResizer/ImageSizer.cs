using Quicksilver.DataConversion;
using SkiaSharp;


namespace Quicksilver.PictureResizer
{
    /// <summary>
    /// Represents a set of factors used for image resizing.
    /// </summary>
    public class ImageResizeFactors
    {
        /// <summary>
        /// Gets or sets the file extension of the resized image (e.g., "jpg", "png").
        /// </summary>
        public string? Extension { get; set; }
        /// <summary>
        /// Gets or sets the scale factor to apply to the image (1.0 for no change).
        /// </summary>
        public double ScaleFactor { get; set; }
        /// <summary>
        /// Gets or sets the quality of the resizing filter (default is SKFilterQuality.Low). Higher quality filters may result in larger image sizes.
        /// </summary>
        public SKFilterQuality Quality { get; set; } = SKFilterQuality.Low;

        /// <summary>
        /// Creates a new ImageResizeFactors object with a smaller scale factor applied.
        /// </summary>
        /// <param name="scaleFactor">The additional scale factor to apply (multiplied by the current scale factor).</param>
        /// <returns>A new ImageResizeFactors object with the updated scale factor.</returns>
        internal ImageResizeFactors SmallerScale(double scaleFactor)
        {
            this.ScaleFactor *= scaleFactor;
            return this;
        }
    }
    /// <summary>
    /// Provides methods for image resizing and generating thumbnails.
    /// </summary>
    public class ImageSizer
    {
        private const string ThumbnailFolderName = "Thumbnail"; // Constant for the thumbnail folder name.

        private static int minAcceptableSize = 512_000; // Minimum acceptable size (in bytes) of a resized image.
        private static int qualityFactor = 100; // Default quality factor for image encoding.

        /// <summary>
        /// Changes the minimum acceptable size for resized images.
        /// </summary>
        /// <param name="size">The new minimum acceptable size in bytes.</param>
        public static void ChangeMinAcceptableFileSize(int size)
        {
            minAcceptableSize = size;
        }
        /// <summary>
        /// Resizes an image stream based on provided ImageResizeFactors and returns a new stream containing the resized image.
        /// </summary>
        /// <param name="imgStream">The stream containing the original image.</param>
        /// <param name="resizeFactors">The object containing resizing factors like scale factor, extension, and quality.</param>
        /// <returns>A new stream containing the resized image data.</returns>
        /// <exception cref="ArgumentException">Thrown if the scale factor is less than or equal to 0.</exception>
        public static Stream Resize(Stream imgStream, ImageResizeFactors resizeFactors)
        {
            if (resizeFactors.ScaleFactor <= 0.0)
            {
                throw new ArgumentException("Negative scale factor is not allowed", nameof(resizeFactors.ScaleFactor));
            }

            if (resizeFactors.ScaleFactor == 1.0) //No resizing needed
            {
                return imgStream;
            }

            using (SKBitmap skImage = SKBitmap.Decode(imgStream))
            using (SKData encodedImage = ResizeCore(skImage, resizeFactors))
            {
                MemoryStream stream = new();
                encodedImage.SaveTo(stream);
                _ = stream.Seek(0, SeekOrigin.Begin);
                return stream;
            };
        }
        /// <summary>
        /// Resizes an image represented as a byte array based on the provided resize factors.
        /// </summary>
        /// <param name="image">The image data as a byte array.</param>
        /// <param name="resizeFactors">An object containing the resize factors (extension, scale factor, and quality).</param>
        /// <returns>A byte array containing the resized image data.</returns>
        /// <exception cref="ArgumentException">Thrown if the scaleFactor is less than or equal to 0.</exception>
        public static byte[] Resize(byte[] image, ImageResizeFactors resizeFactors)
        {
            if (resizeFactors.ScaleFactor <= 0.0)
            {
                throw new ArgumentException("Negative scale factor is not allowed", nameof(resizeFactors.ScaleFactor));
            }

            if (resizeFactors.ScaleFactor == 1.0) //No resizing needed
            {
                return image;
            }

            using (SKBitmap skImage = SKBitmap.Decode(image))
            using (SKData encodedImage = ResizeCore(skImage, resizeFactors))
            {
                return encodedImage.ToArray();
            };
        }
        /// <summary>
        /// Resizes a base64-encoded image string based on the provided resize factors and returns the resized image as a base64-encoded string.
        /// </summary>
        /// <param name="base64Image">The base64-encoded image data.</param>
        /// <param name="resizeFactors">An object containing the resize factors (extension, scale factor, and quality).</param>
        /// <returns>A base64-encoded string containing the resized image data.</returns>
        /// <exception cref="ArgumentException">Thrown if the scaleFactor is less than or equal to 0.</exception>
        public static string Resize(string base64Image, ImageResizeFactors resizeFactors)
        {
            byte[] data = Convert.FromBase64String(base64Image);
            byte[] resizedImageData = Resize(data, resizeFactors);
            return Convert.ToBase64String(resizedImageData);
        }

        /// <summary>
        /// Creates a thumbnail of an image stream based on the provided extension and thumbnail height and returns a new stream containing the thumbnail image.
        /// </summary>
        /// <param name="imgStream">The stream containing the original image.</param>
        /// <param name="extension">The file extension of the thumbnail image (e.g., "jpg", "png").</param>
        /// <param name="thumbnailHeight">The desired height of the thumbnail in pixels (default is 150).</param>
        /// <returns>A new stream containing the thumbnail image data.</returns>
        public static Stream Thumbnail(Stream imgStream, string extension, int thumbnailHeight = 150)
        {
            using (SKBitmap skImage = SKBitmap.Decode(imgStream))
            using (SKData encodedImage = ThumbnailCore(skImage, extension, thumbnailHeight))
            {
                MemoryStream stream = new();
                encodedImage.SaveTo(stream);
                _ = stream.Seek(0, SeekOrigin.Begin);
                return stream;
            }
        }
        /// <summary>
        /// Creates a thumbnail of an image represented as a byte array based on the provided extension and thumbnail height and returns a byte array containing the thumbnail image data.
        /// </summary>
        /// <param name="image">The image data as a byte array.</param>
        /// <param name="extension">The file extension of the thumbnail image (e.g., "jpg", "png").</param>
        /// <param name="thumbnailHeight">The desired height of the thumbnail in pixels (default is 150).</param>
        /// <returns>A byte array containing the thumbnail image data.</returns>
        public static byte[] Thumbnail(byte[] image, string extension, int thumbnailHeight = 150)
        {
            using (SKBitmap skImage = SKBitmap.Decode(image))
            using (SKData encodedImage = ThumbnailCore(skImage, extension, thumbnailHeight))
            {
                return encodedImage.ToArray();
            };
        }
        /// <summary>
        /// Creates a thumbnail of a base64-encoded image string based on the provided extension and thumbnail height and returns the resized image as a base64-encoded string.
        /// </summary>
        /// <param name="base64Image">The base64-encoded image data.</param>
        /// <param name="extension">The file extension of the thumbnail image (e.g., "jpg", "png").</param>
        /// <param name="thumbnailHeight">The desired height of the thumbnail in pixels (default is 150).</param>
        /// <returns>A base64-encoded string containing the thumbnail image data.</returns>
        public static string Thumbnail(string base64Image, string extension, int thumbnailHeight = 150)
        {
            byte[] data = Convert.FromBase64String(base64Image);
            byte[] resizedImageData = Thumbnail(data, extension, thumbnailHeight);
            return Convert.ToBase64String(resizedImageData);
        }


        /// <summary>
        /// Saves a resized version of an image from a stream to a specified location.
        /// </summary>
        /// <param name="imgStream">The stream containing the original image.</param>
        /// <param name="resizeFactors">An object containing the desired scale factor, extension, and quality for the resized image.</param>
        /// <param name="fileName">The name of the resized image file.</param>
        /// <param name="address">The directory path where the resized image will be saved.</param>
        public static void SaveResize(Stream imgStream, ImageResizeFactors resizeFactors, string fileName, string address)
        {
            if (Directory.Exists(address) == false)
                _ = Directory.CreateDirectory(address);
            using (SKBitmap skImage = SKBitmap.Decode(imgStream))
            using (SKData encodedImage = ResizeCore(skImage, resizeFactors))
            {
                using (FileStream stream = File.OpenWrite(Path.Combine(address, $"{fileName}{resizeFactors.Extension}")))
                {
                    encodedImage.SaveTo(stream);
                }
                return;
            }
        }
        /// <summary>
        /// Creates a thumbnail of an image at the specified location and saves it to a dedicated thumbnail folder.
        /// </summary>
        /// <param name="fileAddress">The full path of the original image file.</param>
        /// <param name="thumbPrintHeight">The desired height of the thumbnail in pixels (default is 150).</param>
        /// <exception cref="ArgumentException">Thrown if the fileAddress is null or empty.</exception>
        public static void SaveThumbnail(string fileAddress, int thumbPrintHeight = 150)
        {
            if (string.IsNullOrEmpty(fileAddress))
            {
                throw new ArgumentException($"'{nameof(fileAddress)}' cannot be null or empty.", nameof(fileAddress));
            }

            FileInfo fileInfo = new(fileAddress);
            string directory = Path.Combine(fileInfo.DirectoryName, ThumbnailFolderName);
            if (Directory.Exists(directory) == false)
                _ = Directory.CreateDirectory(directory);

            using (SKBitmap bitmap = SKBitmap.Decode(fileAddress))
            using (SKData data = ThumbnailCore(bitmap, fileInfo.Extension, thumbPrintHeight))
            using (FileStream stream = new(Path.Combine(directory, fileInfo.Name), FileMode.Create, FileAccess.Write))
            {
                data.SaveTo(stream);
            }
        }


        /// <summary>
        /// Performs the core image resizing logic using provided SKBitmap and ImageResizeFactors.
        /// </summary>
        /// <param name="skImage">The SKBitmap object representing the image to resize.</param>
        /// <param name="resizeFactors">The object containing resizing factors like scale factor, extension, and quality.</param>
        /// <returns>An SKData object containing the encoded resized image data.</returns>
        private static SKData ResizeCore(SKBitmap skImage, ImageResizeFactors resizeFactors)
        {
            SKImageInfo newImageInfo = new((int)(skImage.Height * resizeFactors.ScaleFactor), (int)(skImage.Width * resizeFactors.ScaleFactor));

            return _ResizeCore(skImage, newImageInfo, resizeFactors);
        }
        /// <summary>
        /// Creates a thumbnail image from an SKBitmap with a specified height.
        /// </summary>
        /// <param name="skImage">The SKBitmap object representing the image to create a thumbnail from.</param>
        /// <param name="extension">The file extension of the thumbnail image (e.g., "jpg", "png").</param>
        /// <param name="thumbnailHeight">The desired height of the thumbnail in pixels (default is 150).</param>
        /// <returns>An SKData object containing the encoded thumbnail image data.</returns>
        private static SKData ThumbnailCore(SKBitmap skImage, string extension, int thumbnailHeight = 150)
        {

            double ratio = thumbnailHeight / (double)skImage.Height;
            SKImageInfo newImageInfo = new((int)(skImage.Width * ratio), (int)(skImage.Height * ratio));
            ImageResizeFactors imgResizeFactors = new()
            {
                Extension = extension,
                ScaleFactor = skImage.Height > thumbnailHeight ? thumbnailHeight / (double)skImage.Height : 0.5,
                Quality = SKFilterQuality.Low
            };
            return _ResizeCore(skImage, newImageInfo, imgResizeFactors);
        }
        private static SKData _ResizeCore(SKBitmap skImage, SKImageInfo newImageInfo, ImageResizeFactors resizeFactors)
        {
            using (SKBitmap scaledBitmap = skImage.Resize(newImageInfo, resizeFactors.Quality))
            using (SKImage image = SKImage.FromBitmap(scaledBitmap))
            {
                //50 is default quality factor, which is good enough for most cases
                //if image is bigger than 512kb, we will use 50 as quality factor
                //otherwise we will use scaleFactor * 100 as quality factor
                int qualityFactor = resizeFactors.ScaleFactor < 1 ? (resizeFactors.ScaleFactor * 100).ToInt32() : 50;
                SKData encodedImage = image.Encode(MimeTypeMapping.GetSkiaSharpImageFormatFromExtension(resizeFactors.Extension), qualityFactor);
                if (encodedImage.Size <= minAcceptableSize)
                    return encodedImage;

            }

            return ResizeCore(skImage, resizeFactors.SmallerScale(0.75));
        }
    }
    internal static class MimeTypeMapping
    {
        public static string GetMimeTypeFromExtension(string extension)
        {
            //ArgumentException.ThrowIfNullOrEmpty(extension, nameof(extension));
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException(nameof(extension));

            return _mimeTypeMapping.TryGetValue(extension, out string mimeType) ? mimeType : DEFAULT_MIME_TYPE;
        }

        public static SKEncodedImageFormat GetSkiaSharpImageFormatFromExtension(string extension)
        {
            //ArgumentException.ThrowIfNullOrEmpty(extension, nameof(extension));
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException(nameof(extension));

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
