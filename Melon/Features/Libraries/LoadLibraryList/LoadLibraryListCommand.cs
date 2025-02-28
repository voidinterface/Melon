using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Melon.Features.Libraries.LoadLibraryList
{
    public class LoadLibraryListCommand : IRequest
    {
        public required LibraryListViewModel LibraryListViewModel { get; init; }
    }
}
