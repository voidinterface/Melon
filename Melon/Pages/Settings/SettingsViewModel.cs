using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Melon.Common.Core;
using Melon.Features.Locations.CreateLocation;
using Melon.Features.Locations.DeleteLocation;
using Melon.Features.Locations.GetLocations;

namespace Melon.Pages.Settings
{
    public partial class SettingsViewModel : ObservableObject
    {
        public Interaction<FolderPickerOpenOptions, string?> SelectFolderInteraction { get; } = new Interaction<FolderPickerOpenOptions, string?>();

        private readonly IMediator _mediator = null!;
        
        public ObservableCollection<LocationViewModel> Locations { get; } = [];

        public SettingsViewModel()
        {
            Debug.Assert(Design.IsDesignMode);
        }

        public SettingsViewModel(IMediator mediator)
        {
            _mediator = mediator;
            GetLocationsAsync();
        }

        private async void GetLocationsAsync()
        {
            Locations.Clear();
            
            foreach (var location in await _mediator.Send(new GetLocationsQuery()))
            {
                Locations.Add(location);
            }
        }

        [RelayCommand]
        private async Task AddLocationAsync()
        {
            var path = await SelectFolderInteraction.HandleAsync(new FolderPickerOpenOptions
            {
                Title = "Select a folder to add as a location",
            });

            if (path is not null)
            {
                await _mediator.Send(new CreateLocationCommand { Path = path });
                GetLocationsAsync();
            }
        }

        [RelayCommand]
        private async Task DeleteLocationAsync(LocationViewModel location)
        {
            await _mediator.Send(new DeleteLocationCommand { LocationId = location.LocationId });
            GetLocationsAsync();
        }
    }
}
