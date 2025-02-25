using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Models;
using Microsoft.EntityFrameworkCore;

namespace Melon.Database
{
    public class MelonDbContext(DbContextOptions<MelonDbContext> options) : DbContext(options)
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Library> Libraries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MelonDbContext).Assembly);
        }
    }
}
