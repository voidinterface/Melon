using System.Collections.Generic;
using MediatR;
using Melon.Features.Locations.WatchLocations;
using Microsoft.Extensions.DependencyInjection;

namespace Melon.Features.Locations
{
    public static class DependencyInjection
    {
        public static void AddLocationFeature(this IServiceCollection services)
        {
            services.AddSingleton<ILocationWatcherService, LocationWatcherService>();
        }
    }
}