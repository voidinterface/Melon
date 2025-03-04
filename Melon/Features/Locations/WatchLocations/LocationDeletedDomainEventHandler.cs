using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Features.Locations.DomainEvents;

namespace Melon.Features.Locations.WatchLocations
{
    public class LocationDeletedDomainEventHandler(ILocationWatcherService locationWatcherService) : INotificationHandler<LocationDeletedDomainEvent>
    {
        private readonly ILocationWatcherService _locationWatcherService = locationWatcherService;

        public Task Handle(LocationDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            _locationWatcherService.RemoveLocation(notification.Location);
            return Task.CompletedTask;
        }
    }
}
