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
using CommunityToolkit.Mvvm.Messaging;
using Melon.Core;
using Melon.Data;

namespace Melon.ViewModels
{
    public partial class LibraryCatalogViewModel : PageViewModel
    {
        public Interaction<FolderPickerOpenOptions, string?> SelectFolderInteraction { get; } = new Interaction<FolderPickerOpenOptions, string?>();

        public ObservableCollection<LibraryViewModel> Libraries { get; } = [];

        private readonly IMessenger _messenger;

        [ObservableProperty]
        private LibraryViewModel? _selectedLibrary;
        partial void OnSelectedLibraryChanged(LibraryViewModel? value)
        {
            if (value is not null)
            {
                _messenger.Send(new ViewSelectedMessage(value));
            }
        }

        public LibraryCatalogViewModel()
        {
            if (Design.IsDesignMode)
            {
                _messenger = new WeakReferenceMessenger();

                Libraries.Add(new LibraryViewModel { Path = "Library 1" });
                Libraries.Add(new LibraryViewModel { Path = "Library 2" });
            }
            else
            {
                throw new InvalidOperationException("This constructor should only be used in design mode.");
            }
        }

        public LibraryCatalogViewModel(IMessenger messenger)
        {
            PageName = ApplicationPageNames.Library;
            _messenger = messenger;

            Libraries.Add(new LibraryViewModel { Path = "Library 1" });
            Libraries.Add(new LibraryViewModel { Path = "Library 2" });
        }

        [RelayCommand]
        public void SelectPlaylist(ViewModelBase playlist)
        {
            _messenger.Send(new ViewSelectedMessage(playlist));
        }

        [RelayCommand]
        private async Task AddLibraryAsync()
        {
            var path = await SelectFolderInteraction.HandleAsync(new FolderPickerOpenOptions
            {
                Title = "Select a folder to add as a library",
            });

            if (path is not null)
            {
                Libraries.Add(new LibraryViewModel { Path = path.ToString() });
            }
        }

    }
}
