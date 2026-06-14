using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace BasicMvvmSample.Converter;

//converter to add 2 numbers together
public class MathAddConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // throw new NotImplementedException();
        //return sum of value and parameter
        //could improve later by adding validation
        return (decimal?)value + (decimal?)parameter;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // throw new NotImplementedException();
        //to convert back, subtract instead of add
        return (decimal?)value - (decimal?)parameter;
    }
}