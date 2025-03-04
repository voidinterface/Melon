using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Melon.Features.Locations;
using Microsoft.EntityFrameworkCore;

namespace Melon.Features.Locations.DeleteLocation
{
    public class DeleteLocationCommandHandler(MelonDbContext db) : IRequestHandler<DeleteLocationCommand>
    {
        private readonly MelonDbContext _db = db;

        public async Task Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _db.Locations.FirstOrDefaultAsync(l => l.Id == request.LocationId, cancellationToken);

            if (location == null)
            {
                return;
            }

            location = Location.Delete(location);

            _db.Locations.Remove(location);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
