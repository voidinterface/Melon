using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Melon.Features.Songs.GetSongs
{
    public partial class SongViewModel : ObservableObject
    {
        public int SongId { get; }

        public int LocationId { get; }

        public string RelativePath { get; }

        public string Title { get; }

        internal SongViewModel(int songId, int locationId, string relativePath, string title)
        {
            SongId = songId;
            LocationId = locationId;
            RelativePath = relativePath;
            Title = title;
        }

        public static SongViewModel Create(Song song)
        {
            return new SongViewModel(song.Id, song.LocationId, song.RelativePath, song.Title);
        }
    }
}
