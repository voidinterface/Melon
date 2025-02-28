using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using MediatR;
using Melon.Features.Libraries;
using Melon.Features.Libraries.GetAllLibraries;
using Melon.Features.Libraries.LoadLibraryList;

namespace Melon.Pages.Settings
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly IMediator _mediator = null!;
        public LibraryListViewModel LibraryListViewModel { get; } = new();

        public SettingsViewModel()
        {
            Debug.Assert(Design.IsDesignMode);
        }

        public SettingsViewModel(IMediator mediator)
        {
            _mediator = mediator;
            LoadLibrariesAsync();
        }

        private async void LoadLibrariesAsync()
        {
            await _mediator.Send(new LoadLibraryListCommand() { LibraryListViewModel = LibraryListViewModel});
        }
    }
}
