using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Data;
using Melon.ViewModels;

namespace Melon.Views;

public partial class PlaybackControlsView : UserControl
{

    public PlaybackControlsView()
    {
        InitializeComponent();
    }
}
