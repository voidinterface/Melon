using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Microsoft.EntityFrameworkCore;

namespace Melon.Features.Songs.GetSongs
{
    public class GetSongsQueryHandler(MelonDbContext db) : IRequestHandler<GetSongsQuery, List<SongViewModel>>
    {
        private readonly MelonDbContext _db = db;
        public async Task<List<SongViewModel>> Handle(GetSongsQuery request, CancellationToken cancellationToken)
        {
            var songs = new List<SongViewModel>();
            foreach (var song in await _db.Songs.OrderBy(s => s.Id).ToListAsync(cancellationToken: cancellationToken))
            {
                songs.Add(SongViewModel.Create(song));
            }

            return songs;
        }
    }
}
