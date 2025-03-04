using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Melon.Features.Locations;

namespace Melon.Features.Locations.CreateLocation
{
    public class CreateLocationCommandHandler(MelonDbContext db) : IRequestHandler<CreateLocationCommand, int>
    {
        private readonly MelonDbContext _db = db;

        public async Task<int> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = Location.Create(request.Path);

            await _db.AddAsync(location, cancellationToken);


            //TODO: Handle non-unique paths. Validators? UI feedback?
            try
            {
                await _db.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                return -1;
            }


            return location.Id;
        }
    }
}
