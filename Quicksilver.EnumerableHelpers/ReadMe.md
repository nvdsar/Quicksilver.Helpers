# Quicksilver Helpers

This repository was created by Navid Sargheiny as a simple library to simplify enumerable conversions. It specializes in converting enumerables to dynamic and anonymous objects, and has the capability to convert IEnumerables to DataTable and more.

## Installation

Use the package manager [Nuget](https://www.nuget.org/packages/) to install EnumerableHelpers

```bash
dotnet add package Quicksilver.EnumerableHelpers
```

## Usage

There are multiple capabilities available in this library, which I will explain below. Here are some examples of utilizing data conversion:

### String Helpers

``` c#
using Quicksilver.EnumerableHelpers;

           var list =
            [
                new TestObject() { Id = 1, IsValid = true, Name ="FirstObject" },
                new TestObject() { Id = 2, IsValid = false, Name ="SecondObject" },
                new TestObject() { Id = 3, IsValid = true, Name ="ThirdObject" },
            ];
            var result = list.ToDataTable();    //  DataTable
            
            //Convert to dynamic object from a DataTable
            var fullyDynamicObject = list.ToDataTable().ToDynamicList();

            var columns = new List<string>() { "Name", "IsValid" };

            //Convert to dynamic object from an IEnumerable<T>
            var customizedDynamicObject = list.ToDynamicList(columns);
```

## Contributing

Pull requests are welcome. For major changes, please open an issue first
to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License

[MIT](https://choosealicense.com/licenses/mit/)
