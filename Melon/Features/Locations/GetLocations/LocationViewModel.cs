using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Melon.Features.Locations.GetLocations
{
    public partial class LocationViewModel : ObservableObject
    {
        public int LocationId { get; }

        public string Path { get; }

        internal LocationViewModel(int locationId, string path)
        {
            LocationId = locationId;
            Path = path;
        }

        public static LocationViewModel Create(int locationId, string path)
        {
            return new LocationViewModel(locationId, path);
        }
    }
}
