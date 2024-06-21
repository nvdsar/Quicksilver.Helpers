# Quicksilver String Helpers

This repository was created by Navid Sargheiny as a simple library utility to help with strings, especially Persian strings. It has many features, such as providing plural commas, fixing Arabic texts within Farsi texts, and converting numbers into Persian strings.
## Installation

Use the package manager [Nuget](https://www.nuget.org/packages/) to install Quicksilver.StringHelpers

```bash
dotnet add package Quicksilver.StringHelpers
```

## Usage

There are multiple capabilities available in this library, which I will explain below. Here are some examples of utilizing string helpers:

### String Helpers

``` c#
using Quicksilver.StringHelpers;

            var padString = 1.ToPadString(5, '0');  //   "00001"

            var str = "NavidSargheiny";
            var camelCase = str.ToCamelCase();  //   "navidSargheiny"

            str = "My        name    is         Navid          Sargheiny";
            var spaceTrim = str.SpaceTrim();    //  "My name is Navid Sargheiny"

            var texts = new List<string> { "سبب", "گلابی", "توت فرنگی", "هلو", "شلیل" };
            var pluralComma = StringHelpers.GetPluralComma(texts);  //  "سبب، گلابی، توت فرنگی، هلو و شلیل"

            var str = "لطفاً عدد 3322 و حروف ي و ك را اصلاح کن";
            var result = str.ToPersianText();     //    لطفاً عدد ۳۳۲۲ و حروف ی و ک را اصلاح کن

            var integer = 123456789;
            var result = integer.ToPersianText();   //  صد و بیست و سه میلیون و چهارصد و پنجاه و شش هزار و هفتصد و هشتاد و نه
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
