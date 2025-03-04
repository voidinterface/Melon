using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Features.Locations.DomainEvents;

namespace Melon.Features.Locations.WatchLocations
{
    public class LocationCreatedDomainEventHandler(ILocationWatcherService locationWatcherService) : INotificationHandler<LocationCreatedDomainEvent>
    {
        private readonly ILocationWatcherService _locationWatcherService = locationWatcherService;

        public Task Handle(LocationCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _locationWatcherService.AddLocation(notification.Location);
            return Task.CompletedTask;
        }
    }
}
