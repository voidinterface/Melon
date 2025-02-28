using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Melon.Features.Libraries.LoadLibraryList
{
    public partial class LibraryListViewModel : ObservableObject
    {
        public ObservableCollection<string> Paths { get; private set; } = [];

        public void UpdateList(List<string> paths)
        {
            //Paths.Clear();
            //foreach (var path in paths)
            //{
            //    Paths.Add(path);
            //}
            Paths.Add("UPDATED");
        }
    }
}
