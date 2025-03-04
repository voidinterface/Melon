using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Common.Entities;
using Melon.Features.Songs.DomainEvents;

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

        public void Delete()
        {
            AddDomainEvent(new SongDeletedDomainEvent(this));
        }

        public static Song Create(int locationId, string title, string relativePath)
        {
            var song = new Song(locationId, title, relativePath);
            song.AddDomainEvent(new SongCreatedDomainEvent(song));
            return song;
        }
    }
}
