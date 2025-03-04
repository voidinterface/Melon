using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Melon.Features.Locations.DomainEvents
{
    public class LocationScanRequestedDomainEvent(Location location) : INotification
    {
        public Location Location { get; } = location;
    }
}
