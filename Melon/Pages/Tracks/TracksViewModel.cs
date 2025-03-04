using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Melon.Features.Playback.PlaySong;
using Melon.Features.Songs.GetSongs;

namespace Melon.Pages.Tracks
{
    public partial class TracksViewModel : ObservableObject
    {
        private readonly IMediator _mediator = null!;

        public ObservableCollection<SongViewModel> Songs { get; } = [];

        public TracksViewModel()
        {
            Debug.Assert(Design.IsDesignMode);
        }

        public TracksViewModel(IMediator mediator)
        {
            _mediator = mediator;
            LoadSongs();
        }

        public async void LoadSongs()
        {
            var songs = await _mediator.Send(new GetSongsQuery());
            Songs.Clear();
            foreach (var song in songs)
            {
                Songs.Add(song);
            }
        }

        [RelayCommand]
        private async Task PlaySongAsync(SongViewModel song)
        {
            // Play the song
            await _mediator.Send(new PlaySongCommand(song.SongId));
        }
    }
}
