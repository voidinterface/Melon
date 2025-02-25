using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Melon.Models;

namespace Melon.Features.Libraries.GetAllLibraries
{
    public class GetAllLibrariesQuery : IRequest<List<Library>>
    {
    }
}
