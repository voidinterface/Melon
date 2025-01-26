using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Data;
using Melon.Services;

namespace Melon.ViewModels
{
    public partial class LibraryViewModel : ViewModelBase
    {
        private IPlayerService _player;

        [ObservableProperty]
        private string? _path;

        [ObservableProperty]
        private ObservableCollection<SongViewModel> _songs = [];

        [ObservableProperty]
        private SongViewModel? _selectedSong;
        partial void OnSelectedSongChanged(SongViewModel? value)
        {
            if (value is not null)
            {
                _player.stop();
                _player.playSong(value.Path);
            }
        }

        public LibraryViewModel()
        {
            _player = new NAudioPlayerService();
        }

        public LibraryViewModel(IPlayerService player)
        {
            _player = player;
        }

        [RelayCommand]
        public void LoadSongs()
        {
            // Load songs from the library path
            if (Path == null) return;

            // Load songs from the library path
            foreach (var song in Directory.EnumerateFiles(Path, "*.mp3", SearchOption.AllDirectories))
            {
                Songs.Add(new SongViewModel() { Path = song });
            }
        }
    }
}
