using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Melon.Models;

namespace Melon.Features.Libraries.ScanLibrary
{
    public class ScanLibraryCommand : IRequest<int>
    {
        public required Library Library { get; init; }
    }
}
