using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Melon.Features.Locations;
using Melon.Features.Locations.DomainEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;

namespace Melon.Features.Songs.ScanForSongs
{
    public class LocationScanRequestedEventHandler(MelonDbContext db) : INotificationHandler<LocationScanRequestedDomainEvent>
    {
        private readonly MelonDbContext _db = db;

        public async Task Handle(LocationScanRequestedDomainEvent notification, CancellationToken cancellationToken)
        {
            var location = notification.Location;
            
            var songFiles = new HashSet<string>(
                Directory.EnumerateFiles(location.Path, "*.mp3", SearchOption.AllDirectories));

            var storedSongs = await _db.Songs.ToListAsync(cancellationToken: cancellationToken);
            var storedSongFiles = new HashSet<string>(storedSongs.Select(s => Path.Combine(location.Path, s.RelativePath)));

            var newSongs = songFiles
                .Except(storedSongFiles)
                .Select(f => Song.Create(location.Id, Path.GetFileNameWithoutExtension(f), Path.GetRelativePath(location.Path, f)));

            var deletedSongs = storedSongFiles.Except(songFiles).Select(f => storedSongs.First(s => Path.Combine(location.Path, s.RelativePath) == f));
            foreach (var song in deletedSongs)
            {
                song.Delete();
                _db.Songs.Remove(song);
            }


            _db.Songs.AddRange(newSongs);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
} 
