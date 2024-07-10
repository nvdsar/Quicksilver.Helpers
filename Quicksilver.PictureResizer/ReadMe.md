# Quicksilver Helpers

This repository was created by Navid Sargheiny as a simple library for resizing pictures (jpg, jpeg, png) using the SkiaSharp library. It can also provide thumbnail pictures.

## Installation

Use the package manager [Nuget](https://www.nuget.org/packages/) to install Quicksilver.PictureResizer.

```bash
dotnet add package Quicksilver.PictureResizer
```

## Usage

This library provides means to resize images and thumbnails, which I will explain below:

### Picture Resizer

``` c#
using Quicksilver.PictureResizer;

            var extension = Path.GetExtension(fileUploadAddress).ToLowerInvariant();

            Stream stream = File.OpenRead(fileUploadAddress);

            ImageSizer.SaveResize(stream, extension, 0.75, generatedFileName, fileResizePath);
            ImageSizer.SaveThumbnail(Path.Combine(fileResizePath, $"{generatedFileName}{extension}"), 150);
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
