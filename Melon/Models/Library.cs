using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melon.Models
{
    public class Library
    {
        public int LibraryId { get; private set; }
        public string Path { get; private set; }

        public ICollection<Song> Songs { get; private set; } = [];

        internal Library(string path)
        {
            Path = path;
        }

        public static Library Create(string path)
        {
            return new Library(path);
        }
    }
}
