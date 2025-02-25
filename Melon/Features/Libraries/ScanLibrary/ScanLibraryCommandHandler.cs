using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Melon.Models;

namespace Melon.Features.Libraries.ScanLibrary
{
    public class ScanLibraryCommandHandler(ILibraryScanner scanner, MelonDbContext db) : IRequestHandler<ScanLibraryCommand, int>
    {
        private readonly ILibraryScanner _scanner = scanner;
        private readonly MelonDbContext _db = db;

        public async Task<int> Handle(ScanLibraryCommand request, CancellationToken cancellationToken)
        {
            List<Song> songs = _scanner.ScanLibrary(request.Library);

            await _db.AddRangeAsync(songs, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);

            return songs.Count;
        }
    }
}
