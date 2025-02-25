using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Models;

namespace Melon.Features.Libraries.ScanLibrary
{
    public class LibraryScanner : ILibraryScanner
    {
        public List<Song> ScanLibrary(Library library)
        {
            var songs = new List<Song>();
            foreach (var file in Directory.EnumerateFiles(library.Path, "*.mp3", SearchOption.AllDirectories))
            {
                var relativePath = file[(library.Path.Length)..];

                //TODO: Scan for metadata
                var title = Path.GetFileNameWithoutExtension(file);
                songs.Add(Song.Create(relativePath, title, library));
            }

            return songs;
        }
    }
}
