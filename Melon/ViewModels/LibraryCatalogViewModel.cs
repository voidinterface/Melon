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
using Melon.Factories;

namespace Melon.ViewModels
{
    public partial class LibraryCatalogViewModel : PageViewModel
    {
        public Interaction<FolderPickerOpenOptions, string?> SelectFolderInteraction { get; } = new Interaction<FolderPickerOpenOptions, string?>();

        public ObservableCollection<LibraryViewModel> Libraries { get; } = [];

        private readonly IMessenger _messenger;
        private readonly LibraryFactory _libraryFactory = null!;

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

            }
            else
            {
                throw new InvalidOperationException("This constructor should only be used in design mode.");
            }
        }

        public LibraryCatalogViewModel(IMessenger messenger, LibraryFactory libraryFactory)
        {
            PageName = ApplicationPageNames.Library;
            _messenger = messenger;

            _libraryFactory = libraryFactory;
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
                Libraries.Add(_libraryFactory.GetLibraryViewModel(c => c.Path = path.ToString()));
            }
        }

    }
}
