using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Melon.Database.Configurations
{
    internal class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasIndex(s => new { s.RelativePath, s.LibraryId })
                .IsUnique();

            //    builder.HasOne<Library>()
            //        .WithMany()
            //        .IsRequired();
        }
    }
}
