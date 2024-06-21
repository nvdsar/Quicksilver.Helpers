# Quicksilver Helpers

This repository was created by Navid Sargheiny as a simple library to simplify data conversion, especially value type conversion. It has features such as understanding null values and providing default values in null cases.

## Installation

Use the package manager [Nuget](https://www.nuget.org/packages/) to install Quicksilver.DataConversion

```bash
dotnet add package Quicksilver.DataConversion
```

## Usage

There are multiple capabilities available in this library, which I will explain below. Here are some examples of utilizing data conversion:

### String Helpers

``` c#
using Quicksilver.DataConversion;

            var d = "2.34";
            d.ToDouble(); //2.34

            double? d1 = null;
            d1.ToDouble(1.44); //1.44

            d1.ToNullableDouble(); //null
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
