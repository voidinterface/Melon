using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Microsoft.EntityFrameworkCore;

namespace Melon.Features.Locations.GetLocations
{
    public class GetLocationsQueryHandler(MelonDbContext db) : IRequestHandler<GetLocationsQuery, List<LocationViewModel>>
    {
        private readonly MelonDbContext _db = db;
        public async Task<List<LocationViewModel>> Handle(GetLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _db.Locations
                .OrderBy(l => l.Id)
                .ToListAsync(cancellationToken);

            List<LocationViewModel> result = [];

            foreach (var location in locations)
            {
                result.Add(LocationViewModel.Create(
                    location.Id,
                    location.Path));
            }

            return result;
        }
    }
}
