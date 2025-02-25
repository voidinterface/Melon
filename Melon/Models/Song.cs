using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Melon.Models
{
    public class Song
    {
        public int SongId { get; private set; }
        public string Title { get; private set; }

        public string RelativePath { get; private set; }

        public Library Library { get; private set; } = null!;

        public int LibraryId { get; private set; }

        internal Song(string relativePath, string title, int libraryId)
        {
            Title = title;
            RelativePath = relativePath;
            LibraryId = libraryId;
        }

        public static Song Create(string relativePath, string title, Library library)
        {
            return new Song(relativePath, title, library.LibraryId);
        }

        public string GetFullPath()
        {
            return System.IO.Path.Combine(Library.Path, RelativePath);
        }
    }
}
