using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Data;

namespace Melon.ViewModels
{
    public partial class PlaylistCatalogViewModel : PageViewModel
    {
        public ObservableCollection<PlaylistViewModel> Playlists { get; } = [];

        private readonly IMessenger _messenger;

        [ObservableProperty]
        private PlaylistViewModel? _selectedPlaylist;
        partial void OnSelectedPlaylistChanged(PlaylistViewModel? value)
        {
            if (value is not null)
            {
                _messenger.Send(new ViewSelectedMessage(value));
            }
        }

        public PlaylistCatalogViewModel()
        {
            if (Design.IsDesignMode)
            {
                _messenger = new WeakReferenceMessenger();

                Playlists.Add(new PlaylistViewModel { Name = "Playlist 1" });
                Playlists.Add(new PlaylistViewModel { Name = "Playlist 2" });
            }
            else
            {
                throw new InvalidOperationException("This constructor should only be used in design mode.");
            }
        }

        public PlaylistCatalogViewModel(IMessenger messenger)
        {
            PageName = ApplicationPageNames.Playlists;
            _messenger = messenger;

            Playlists.Add(new PlaylistViewModel { Name = "Playlist 1" });
            Playlists.Add(new PlaylistViewModel { Name = "Playlist 2" });
        }

        [RelayCommand]
        public void SelectPlaylist(ViewModelBase playlist)
        {
            _messenger.Send(new ViewSelectedMessage(playlist));
        }

        [RelayCommand]
        private void AddPlaylist()
        {
            Playlists.Add(new PlaylistViewModel() { Name = "New Playlist" });
        }
    }
}
