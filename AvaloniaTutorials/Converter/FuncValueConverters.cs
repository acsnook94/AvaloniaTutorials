using System.Runtime.CompilerServices;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace AvaloniaTutorials.Converter;

public static class FuncValueConverters
{
    //Gets a Converter that returns a parsed Brush for a given input. Retyurns null if input not parsed successfully
    public static FuncValueConverter<string?, Brush?> StringToBrushConverter { get; } =
        new FuncValueConverter<string?, Brush?>(s =>
        {
            //output variable
            Color color;

            //try parse color. If not successful, try to add a leading '#'
            if (Color.TryParse(s, out color) || Color.TryParse($"#{s}", out color))
            {
                return new SolidColorBrush(color);
            }

            //if string not convertible, return null
            return null;
        });
}