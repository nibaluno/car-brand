using System.Globalization;

namespace CarBrand.Mobile.ValueConverters;

public class PriceToColorValueConverter : IValueConverter
{
    private const decimal PriceThreshold = 10000m;

    public object Convert(
        object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        if (value is decimal price && price < PriceThreshold)
            return Colors.LightPink;

        return Colors.Transparent;
    }

    public object ConvertBack(
        object value, Type targetType,
        object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}