﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Melon.Features.Libraries
{
    public partial class LibraryViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _path;

    }
}
