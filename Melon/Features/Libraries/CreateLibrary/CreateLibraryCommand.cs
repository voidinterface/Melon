using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Melon.Features.Libraries.CreateLibrary
{
    public class CreateLibraryCommand : IRequest<int>
    {
        public required string Path { get; init; } 
    }
}
