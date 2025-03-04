using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Melon.Features.Locations.GetLocations
{
    public class GetLocationsQuery : IRequest<List<LocationViewModel>>
    {
    }
}
