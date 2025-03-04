using MediatR;

namespace Melon.Features.Locations.DomainEvents
{
    public class LocationCreatedDomainEvent : INotification
    {
        public Location Location { get; }

        internal LocationCreatedDomainEvent(Location location)
        {
            Location = location;
        }
    }
}
