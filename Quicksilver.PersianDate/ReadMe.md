# Quicksilver Helpers

This repository was created by Navid Sargheiny as a simple library to utilize Persian date capabilities. It has many features, such as providing different types of Persian dates and offering means to extract Persian dates as Persian text.

## Installation

Use the package manager [Nuget](https://www.nuget.org/packages/) to install Quicksilver.PersianDate.

```bash
dotnet add package Quicksilver.PersianDate
```

## Usage

There are multiple capabilities available in this library, which I will explain below. Here are some examples of utilizing string helpers:

### Persian Date Helpers

``` c#
using Quicksilver.PersianDate;

            var date = new DateTime(2024, 6, 17, 12, 15, 16);
            var persianDate = date.ToPersianDate(DateFormat.yyyy_MM_dd); //  "1403/03/28"
            persianDate = date.ToPersianDate(DateFormat.yy_MM_dd);   //    "03/03/28"
            persianDate = date.ToPersianDate(DateFormat.yyMMdd);   //    "030328"
            persianDate = date.ToPersianDate(DateFormat.yyyyMMdd);   //  "14030328"
            persianDate = date.ToPersianDate(DateFormat.yyyy_MM_dd_HH_mm_ss); //  "1403/03/28 12:15:16"

            var pDateString = "date is 1403/03/28 hh 22 text";
            var date = persianDate.ToGregorianDate(); // new DateTime(2024, 6, 17);

            var firstDay = PersianDate.GetFirstDayOfSolarHijri(1403); // new DateTime(2024, 3, 20);

            var Monday = PersianDate.GetDayOfWeek(2);   //    "دوشنبه"
            var khordad = PersianDate.GetMonthName(3);  //      "خرداد"

            var date = new DateTime(2024, 6, 21, 21, 15, 16);
            var result = PersianDate.GetPersianDateText(date);      //      جمعه، اول تیر ماه یک هزار و چهارصد و سه
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
