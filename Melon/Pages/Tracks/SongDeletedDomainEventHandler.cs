﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Features.Songs.DomainEvents;

namespace Melon.Pages.Tracks
{
    internal class SongDeletedDomainEventHandler(TracksViewModel tracksViewModel) : INotificationHandler<SongDeletedDomainEvent>
    {
        private readonly TracksViewModel _tracksViewModel = tracksViewModel;
        public Task Handle(SongDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            _tracksViewModel.LoadSongs();
            return Task.CompletedTask;
        }
    }
}
