using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Melon.Common.Entities;
using Melon.Features.Locations;
using Melon.Features.Songs;
using Microsoft.EntityFrameworkCore;

namespace Melon.Database
{
    public class MelonDbContext(DbContextOptions<MelonDbContext> options, IMediator mediator) : DbContext(options)
    {
        private readonly IMediator _mediator = mediator;

        public DbSet<Location> Locations { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MelonDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            await PublishDomainEventsAsync();
            

            return result;
        }

        private async Task PublishDomainEventsAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(e => e.Entity)
                .SelectMany(e =>
                {
                    List<INotification> events = [.. e.DomainEvents];

                    e.ClearDomainEvents();

                    return events;
                })
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
