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
            var songs = new List<Song>();

            foreach (var file in Directory.EnumerateFiles(location.Path, "*.mp3", SearchOption.AllDirectories))
            {
                var relativePath = file[location.Path.Length..];

                if (await _db.Songs.AnyAsync(s => s.LocationId == location.Id && s.RelativePath == relativePath, cancellationToken: cancellationToken))
                {
                    continue;
                }

                songs.Add(Song.Create(location.Id, Path.GetFileNameWithoutExtension(file), relativePath));
            }

            _db.Songs.AddRange(songs);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
} 
