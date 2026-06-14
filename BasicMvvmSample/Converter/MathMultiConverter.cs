using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace BasicMvvmSample.Converter;

public class MathMultiConverter : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        // throw new NotImplementedException();
        //validate if provide values are valid. Need at least 3
        //First is operator, other 2 are decimals
        if (values.Count != 3)
        {
            //inform developer
            Trace.WriteLine("Exactly three values expected.");
            
            //return this instead of throwing an exception
            //it is possible to return a binding operation WITH an exception
            return BindingOperations.DoNothing;
        }
        
        //first item is operation type
        // string op = values[0] as string ?? "+";
        
        // char op = System.Convert.ToChar(values[0]);


        char op = (values[0] as string ?? "+")[0];
        // char op = values[0].ToString()[0];
        
        //store other values
        decimal value1 = values[1] as decimal? ?? 0;
        decimal value2 = values[2] as decimal? ?? 0;
        
        //perform operation based on type
        switch (op)
        {
            case '+':
                return value1 + value2;
            case '-':
                return value1 - value2;
            case '*':
                return value1 * value2;
            case '/':
                //check for divide by 0, not allowed
                if (value2 == 0)
                {
                    return new BindingNotification(new DivideByZeroException("Don't do that, idiot!"), BindingErrorType.Error);
                }

                return value1 / value2;
        }
        
        //if we reach this, something went wrong. Return error notification
        return new BindingNotification(new InvalidOperationException("Something went wrong"), BindingErrorType.Error);
    }
}