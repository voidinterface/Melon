using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Data;
using Melon.ViewModels;

namespace Melon.Views;

public partial class LibraryCatalogView : UserControl
{
    IDisposable? _selectFolderInteractionDisposable;

    public LibraryCatalogView()
    {
        InitializeComponent();
    }

    private async Task<string?> InteractionHandler(FolderPickerOpenOptions input)
    {
        var topLevel = TopLevel.GetTopLevel(this);

        if (topLevel == null)
        {
            return null;
        }

        var folder = await topLevel.StorageProvider.OpenFolderPickerAsync(input);

        if (folder == null || folder[0] == null)
        {
            return null;
        }

        return folder[0].TryGetLocalPath();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        // Dispose any old handler
        _selectFolderInteractionDisposable?.Dispose();

        if (DataContext is LibraryCatalogViewModel vm)
        {
            // register the interaction handler
            _selectFolderInteractionDisposable =
                vm.SelectFolderInteraction.RegisterHandler(InteractionHandler);
        }

        base.OnDataContextChanged(e);
    }
}
