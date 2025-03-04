using MediatR;

namespace Melon.Features.Locations.DomainEvents
{
    public class LocationDeletedDomainEvent : INotification
    {
        public Location Location { get; }

        internal LocationDeletedDomainEvent(Location location)
        {
            Location = location;
        }
    }
}
