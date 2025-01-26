using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Melon.ViewModels
{
    public partial class PlaylistViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string? _name;

        //[RelayCommand] 

        public PlaylistViewModel()
        {

        }
    }
}
