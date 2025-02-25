using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Melon.Models;

namespace Melon.Features.Libraries.CreateLibrary
{
    public class CreateLibraryCommandHandler : IRequestHandler<CreateLibraryCommand, int>
    {
        private readonly MelonDbContext _db;

        public CreateLibraryCommandHandler(MelonDbContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(CreateLibraryCommand request, CancellationToken cancellationToken)
        {
            var library = Library.Create(request.Path);

            await _db.AddAsync(library, cancellationToken);

            await _db.SaveChangesAsync(cancellationToken);

            return library.LibraryId;
        }
    }
}
