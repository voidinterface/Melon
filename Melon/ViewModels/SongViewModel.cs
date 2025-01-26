using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Melon.ViewModels
{
    public partial class SongViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _path;
    }
}
