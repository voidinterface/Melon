using System.Threading.Tasks;
using Melon.Features.Locations;

namespace Melon.Features.Locations.WatchLocations
{
    public interface ILocationWatcherService
    {
        public Task Start();
        public void AddLocation(Location location);

        public void RemoveLocation(Location location);
    }
}
