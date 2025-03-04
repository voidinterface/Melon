using System;
using System.Collections.Generic;
using System.Linq;
using Melon.Common.Entities;
using Melon.Features.Locations.DomainEvents;

namespace Melon.Features.Locations
{
    public class Location : Entity
    {
        public string Path { get; private set; }

        internal Location(string path)
        {
            Path = path;
        }

        public static Location Create(string path)
        {
            var location = new Location(path);

            location.AddDomainEvent(new LocationCreatedDomainEvent(location));
            location.AddDomainEvent(new LocationScanRequestedDomainEvent(location));

            return location;
        }

        public static Location Delete(Location location)
        {
            location.AddDomainEvent(new LocationDeletedDomainEvent(location));
            return location;
        }

        public static Location RequestLocationScan(Location location)
        {
            location.AddDomainEvent(new LocationScanRequestedDomainEvent(location));
            return location;
        }
    }
}
