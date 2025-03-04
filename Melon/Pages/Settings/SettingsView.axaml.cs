using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;

namespace Melon.Pages.Settings;

public partial class SettingsView : UserControl
{
    IDisposable? _selectFolderInteractionDisposable;

    public SettingsView()
    {
        InitializeComponent();
    }

    private async Task<string?> InteractionHandler(FolderPickerOpenOptions options)
    {
        var topLevel = TopLevel.GetTopLevel(this);

        if (topLevel == null)
        {
            return null;
        }

        var folder = await topLevel.StorageProvider.OpenFolderPickerAsync(options);

        if (folder == null || folder.Count < 1)
        {
            return null;
        }

        return folder[0].TryGetLocalPath();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        // Dispose any old handler
        _selectFolderInteractionDisposable?.Dispose();

        if (DataContext is SettingsViewModel vm)
        {
            // register the interaction handler
            _selectFolderInteractionDisposable =
                vm.SelectFolderInteraction.RegisterHandler(InteractionHandler);
        }

        base.OnDataContextChanged(e);
    }
}