using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Melon.Features.Locations.CreateLocation
{
    public class CreateLocationCommand : IRequest<int>
    {
        public required string Path { get; init; }
    }
}
