using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melon.Features.Locations.WatchLocations
{
    public class LocationFileWatcher(string path) : FileSystemWatcher(path)
    {
        public int LocationId { get; init; }
    }
}
