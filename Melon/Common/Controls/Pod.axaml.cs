using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Melon.Common.Controls;

public partial class Pod : Border
{
    public Pod()
    {
        BorderThickness = new Thickness(2);
        CornerRadius = new CornerRadius(3);
    }
}