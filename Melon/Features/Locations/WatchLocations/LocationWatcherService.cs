using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Xaml.Interactions.Custom;
using MediatR;
using Melon.Database;
using Melon.Features.Locations;
using Microsoft.EntityFrameworkCore;

namespace Melon.Features.Locations.WatchLocations
{
    public class LocationWatcherService(MelonDbContext db) : ILocationWatcherService
    {
        private readonly MelonDbContext _db = db;
        private readonly Dictionary<int, LocationFileWatcher> _watchers = new();

        public async Task Start()
        {
            _watchers.Clear();

            foreach (var location in await _db.Locations.ToListAsync())
            {
                await RequestUpdate(location);
                StartWatching(location);
            }
        }

        private async Task RequestUpdate(Location location)
        {
            location = Location.RequestLocationScan(location);

            _db.Locations.Update(location);

            await _db.SaveChangesAsync();
        }

        private void StartWatching(Location location)
        {
            var watcher = new LocationFileWatcher(location.Path)
            {
                Filter = "*.mp3",
                IncludeSubdirectories = true,
                LocationId = location.Id
            };
            watcher.Created += OnUpdated;
            watcher.Deleted += OnUpdated;
            watcher.Renamed += OnUpdated;
            watcher.EnableRaisingEvents = true;
            _watchers.Add(location.Id, watcher);
        }

        private void StopWatching(Location location)
        {
            if (_watchers.TryGetValue(location.Id, out var watcher))
            {
                watcher.EnableRaisingEvents = false;
                watcher.Created -= OnUpdated;
                watcher.Deleted -= OnUpdated;
                watcher.Renamed -= OnUpdated;
                watcher.Dispose();
                _watchers.Remove(location.Id);
            }
        }

        private async void OnUpdated(object sender, FileSystemEventArgs e)
        {
            int locationId;
            if (sender is LocationFileWatcher watcher)
            {
                locationId = watcher.LocationId;
            }
            else
            {
                return;
            }

            var location = await _db.Locations.FirstOrDefaultAsync(ls => ls.Id == locationId);
            if (location == null)
            {
                return;
            }

            await RequestUpdate(location);
        }

        public void AddLocation(Location location)
        {
            StartWatching(location);
        }

        public void RemoveLocation(Location location)
        {
            StopWatching(location);
        }
    }
}
