# Quicksilver Helpers

This repository was created by Navid Sargheiny as a simple library Powerful and Flexible Image Resizing and Thumbnail Creation.

This repository also contains utilities for applying watermarks. Below are descriptions of the available classes and their public methods, along with examples of how to use them.
## Installation

Use the package manager [Nuget](https://www.nuget.org/packages/) to install Quicksilver.PictureResizer.

```bash
dotnet add package Quicksilver.PictureResizer
```

## Usage

This library provides means to resize images and thumbnails, which I will explain below:

### 1. `Picture Resizer`

**Key Features:**

* Resize images based on scale factors and desired extensions.
* Generate thumbnails with customizable heights.
* Handles both byte arrays and base64-encoded image data.
* Provides efficient in-memory processing.
* Offers options to save resized and thumbnail images to specific locations.


``` csharp
using Quicksilver.PictureResizer;

var extension = Path.GetExtension(fileUploadAddress).ToLowerInvariant();

Stream stream = File.OpenRead(fileUploadAddress);

ImageSizer.SaveResize(stream, extension, 0.75, generatedFileName, fileResizePath);
ImageSizer.SaveThumbnail(Path.Combine(fileResizePath, $"{generatedFileName}{extension}"), 150);
```

### 2. `Watermark`

The `Watermark` class allows you to apply watermarks to images. The class provides methods to add watermarks to images using various formats.

### Public Methods:

- **`AddWatermark(byte[] image, params Watermark[] watermarks)`**  
  Applies one or more watermarks to an image provided as a byte array.

  **Parameters:**
  - `image`: A byte array representing the image to which the watermark(s) will be applied.
  - `watermarks`: One or more `Watermark` objects to be applied to the image.

  **Example Usage:**
  ```csharp
  using Quicksilver.PictureResizer;
  byte[] imageData = File.ReadAllBytes("path/to/image.jpg");
  byte[] waterMark1 = File.ReadAllBytes("path/to/wm1.jpg");
  byte[] waterMark2 = File.ReadAllBytes("path/to/wm2.jpg");
  Watermark[] watermark = [
                new Watermark(){Image = waterMark1,Position = new System.Drawing.Point(10, 10),ReverseX = true,ReverseY = true,},
                new Watermark(){Image = waterMark2,Position = new System.Drawing.Point(10, 10),ReverseX = true,ReverseY = false}
                ];
  byte[] result = WatermarkProcessor.AddWatermark(imageData, watermark);
  File.WriteAllBytes("path/to/output.jpg", result);
  ```


## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
