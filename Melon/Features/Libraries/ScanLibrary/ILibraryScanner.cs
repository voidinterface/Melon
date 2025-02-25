using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Models;

namespace Melon.Features.Libraries.ScanLibrary
{
    public interface ILibraryScanner
    {
        public List<Song> ScanLibrary(Library library);
    }
}
