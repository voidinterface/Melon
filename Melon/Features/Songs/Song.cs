using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Common.Entities;

namespace Melon.Features.Songs
{
    public class Song : Entity
    {
        public int LocationId { get; private set; }

        public string Title { get; private set; }

        public string RelativePath { get; private set; }

        internal Song(int locationId, string title, string relativePath)
        {
            Title = title;
            LocationId = locationId;
            RelativePath = relativePath;
        }

        public string GetFullPath()
        {
            return "";
        }

        public static Song Create(int locationId, string title, string relativePath)
        {
            return new Song(locationId, title, relativePath);
        }
    }
}
