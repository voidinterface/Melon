using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Xaml.Interactivity;

namespace Melon.Common.Behaviors
{
    public class SliderSeekingBehavior : Behavior<Slider>
    {
        public static readonly StyledProperty<ICommand?> SeekStartedCommandProperty = AvaloniaProperty.Register<SliderSeekingBehavior, ICommand?>(nameof(SeekStartedCommand));

        public static readonly StyledProperty<ICommand?> SeekCompletedCommandProperty = AvaloniaProperty.Register<SliderSeekingBehavior, ICommand?>(nameof(SeekCompletedCommand));
        protected override void OnAttached()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.AddHandler(InputElement.PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel);
                AssociatedObject.AddHandler(InputElement.PointerReleasedEvent, OnPointerReleased, RoutingStrategies.Tunnel);
            }
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.RemoveHandler(InputElement.PointerPressedEvent, OnPointerPressed);
                AssociatedObject.RemoveHandler(InputElement.PointerReleasedEvent, OnPointerReleased);
            }
        }

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            SeekStartedCommand?.Execute(null);
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            SeekCompletedCommand?.Execute(null);
        }

        public ICommand? SeekStartedCommand
        {
            get => GetValue(SeekStartedCommandProperty);
            set => SetValue(SeekStartedCommandProperty, value);
        }

        public ICommand? SeekCompletedCommand
        {
            get => GetValue(SeekCompletedCommandProperty);
            set => SetValue(SeekCompletedCommandProperty, value);
        }
    }
}
