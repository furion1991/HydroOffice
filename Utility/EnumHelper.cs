using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace HydroOffice.Utility;

public static class EnumHelper
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}

public class EnumDescriptionConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? enumValue.ToString();
        }
        return value?.ToString() ?? string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException(); // not needed for display only
}