using System.Globalization;

namespace CarBrand.Mobile.ValueConverters;

public class IdToImageValueConverter : IValueConverter
{
    private static string ImagesFolder => Path.Combine(FileSystem.Current.AppDataDirectory, "Images");

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int id && id > 0)
        {
            foreach (var ext in new[] { ".jpg", ".jpeg", ".png", ".heic" })
            {
                var path = Path.Combine(ImagesFolder, $"{id}{ext}");
                if (File.Exists(path))
                {
                    try
                    {
                        var bytes = File.ReadAllBytes(path);
                        return ImageSource.FromStream(() => new MemoryStream(bytes));
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }
        return "no_image.png";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}