using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Melon.Features.Playback.PlaySong
{
    public class PlaySongCommand(int songId) : IRequest
    {
        public int SongId { get; } = songId;
    }
}
