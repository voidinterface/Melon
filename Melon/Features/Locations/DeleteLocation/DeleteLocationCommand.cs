using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Melon.Features.Locations.DeleteLocation
{
    public class DeleteLocationCommand : IRequest
    {
        public required int LocationId { get; init; }
    }
}
