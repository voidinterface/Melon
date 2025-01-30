using Avalonia.Controls;
using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Melon.Data;
using Melon.Factories;
using CommunityToolkit.Mvvm.Messaging;
using System.Xml.Serialization;

namespace Melon.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentView = null!;

    [ObservableProperty]
    private ViewModelBase _playbackControls;

    private readonly PageFactory _pageFactory = null!;

    private IMessenger _messenger;

    public MainViewModel()
    {
        if (Design.IsDesignMode)
        {
            _messenger = new WeakReferenceMessenger();
            CurrentView = new PlaylistCatalogViewModel();
            _playbackControls = new PlaybackControlsViewModel();
        }
        else
        {
            throw new InvalidOperationException("This constructor should only be used in design mode.");
        }
    }

    public MainViewModel(PageFactory pageFactory, IMessenger messenger, PlaybackControlsViewModel playbackControls)
    {
        _pageFactory = pageFactory;

        _messenger = messenger;
        _messenger.Register<ViewSelectedMessage>(this, (recipient, message) =>
        {
            NavigateToView(message.Value);
        });

        _playbackControls = playbackControls;

        NavigateToPage(ApplicationPageNames.Library);
    }

    [RelayCommand]
    private void NavigateToPage(ApplicationPageNames pageName)
    {
        NavigateToView(_pageFactory.GetPageViewModel(pageName));
    }

    [RelayCommand]
    private void NavigateToView(ViewModelBase viewModel)
    {
        CurrentView = viewModel;
    }

    [RelayCommand]
    private void GoToLibraryCatalog()
    {
        NavigateToPage(ApplicationPageNames.Library);
    }

    [RelayCommand]
    private void GoToPlaylistCatalog()
    {
        NavigateToPage(ApplicationPageNames.Playlists);
    }
}
