using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Melon.Models;
using Microsoft.EntityFrameworkCore;

namespace Melon.Features.Libraries.GetAllLibraries
{
    public class GetAllLibrariesQueryHandler(MelonDbContext db) : IRequestHandler<GetAllLibrariesQuery, List<Library>>
    {
        private readonly MelonDbContext _db = db;
        public async Task<List<Library>> Handle(GetAllLibrariesQuery request, CancellationToken cancellationToken)
        {
            //TODO: This should be viewmodel not model
            var libraries = await _db.Libraries
                .OrderBy(l => l.LibraryId)
                .ToListAsync(cancellationToken);

            return libraries;
        }
    }
}
