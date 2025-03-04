using Avalonia.Controls;
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Features.Playback.PlaybackControlsBar;
using System.Diagnostics;
using Melon.Pages.Settings;
using Melon.Pages.Tracks;

namespace Melon.Pages;

public partial class MainViewModel : ObservableObject
{
    public ObservableObject PlaybackControlsBar { get; private set; }

    public ObservableObject Settings { get; private set; }

    public ObservableObject Tracks { get; private set; }

    public MainViewModel() : this(new PlaybackControlsBarViewModel(), new SettingsViewModel(), new TracksViewModel())
    {
        Debug.Assert(Design.IsDesignMode);
    }

    public MainViewModel(PlaybackControlsBarViewModel playbackControlsBar, SettingsViewModel settings, TracksViewModel tracks)
    {
        PlaybackControlsBar = playbackControlsBar;
        Settings = settings;
        Tracks = tracks;
    }
}
