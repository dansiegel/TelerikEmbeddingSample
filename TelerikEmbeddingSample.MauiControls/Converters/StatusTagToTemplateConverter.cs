using System.Globalization;
using TelerikEmbeddingSample.MauiControls.Common;

namespace TelerikEmbeddingSample.MauiControls.Converters;

public class StatusTagToTemplateConverter : IValueConverter
{
    public DataTemplate NormalTemplate { get; set; } = default!;
    public DataTemplate NewTemplate { get; set; } = default!;
    public DataTemplate UpdatedTemplate { get; set; } = default!;
    public DataTemplate BetaTemplate { get; set; } = default!;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var statusTagTemplate = ((StatusType)value) switch
        {
            StatusType.New => NewTemplate,
            StatusType.Updated => UpdatedTemplate,
            StatusType.Beta => BetaTemplate,
            _ => NormalTemplate,
        };

        return statusTagTemplate.CreateContent();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
