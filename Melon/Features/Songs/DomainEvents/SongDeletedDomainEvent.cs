using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Melon.Features.Songs.DomainEvents
{
    public class SongDeletedDomainEvent(Song song) : INotification
    {
        public Song Song { get; } = song;
    }
}
