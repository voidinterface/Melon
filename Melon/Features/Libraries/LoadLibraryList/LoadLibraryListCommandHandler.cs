using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Database;
using Microsoft.EntityFrameworkCore;

namespace Melon.Features.Libraries.LoadLibraryList
{
    public class LoadLibraryListCommandHandler(MelonDbContext db) : IRequestHandler<LoadLibraryListCommand>
    {
        private readonly MelonDbContext _db = db;
        public async Task Handle(LoadLibraryListCommand request, CancellationToken cancellationToken)
        {
            //var libraries = await _db.Libraries
            //    .OrderBy(l => l.LibraryId)
            //    .Select(l => l.Path)
            //    .ToListAsync(cancellationToken);
            await Task.Run(() => request.LibraryListViewModel.UpdateList(["libraries"]));
        }
    }
}
