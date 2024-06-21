# Quicksilver Helpers

This repository was created by Navid Sargheiny as a simple and general utility with various extensions and static utilities. The initial commit includes DataConversion extensions, Persian Date extensions (useful for Solar Hijri date), and some string helpers (specifically for the Persian language).


## Usage

There are multiple capabilities available in this helper library, which I will explain below. Here are some examples of utilizing string helpers:

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
```

### PersianDate

``` c#
using Quicksilver.PersianDate;

            var date = new DateTime(2024, 6, 17, 12, 15, 16);
            var persianDate = date.ToPersianDate(); //  "1403/03/28"
            persianDate = date.To2DigitYearPersianDate();   //    "03/03/28"
            persianDate = date.To6DigitPersianDate();   //    "030328"
            persianDate = date.To8DigitPersianDate();   //  "14030328"
            persianDate = date.ToPersianDateTime(); //  "1403/03/28 12:15:16"

            var pDateString = "date is 1403/03/28 hh 22 text";
            var date = persianDate.ToGregorianDate(); // new DateTime(2024, 6, 17);

            var firstDay = PersianDate.GetFirstDayOfSolarHijri(1403); // new DateTime(2024, 3, 20);

            var Monday = PersianDate.GetDayOfWeek(2);   //    "دوشنبه"
            var khordad = PersianDate.GetMonthName(3);  //      "خرداد"
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
