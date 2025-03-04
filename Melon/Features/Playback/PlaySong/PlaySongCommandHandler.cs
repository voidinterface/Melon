using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Microsoft.EntityFrameworkCore;

namespace Melon.Features.Playback.PlaySong
{
    public class PlaySongCommandHandler(MelonDbContext db, IPlayerService service) : IRequestHandler<PlaySongCommand>
    {
        private readonly MelonDbContext _db = db;
        private readonly IPlayerService _service = service;

        public async Task Handle(PlaySongCommand request, CancellationToken cancellationToken)
        {
            var song = await _db.Songs.FirstOrDefaultAsync(s => s.Id == request.SongId, cancellationToken: cancellationToken);

            if (song == null)
            {
                return;
            }

            var library = await _db.Locations.FirstOrDefaultAsync(l => l.Id == song.LocationId, cancellationToken: cancellationToken);

            if (library == null)
            {
                return;
            }

            _service.Play(library.Path + song.RelativePath);
        }
    }
}
