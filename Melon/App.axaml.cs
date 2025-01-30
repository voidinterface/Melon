using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Data;
using Melon.Factories;
using Melon.Services;
using Melon.ViewModels;
using Melon.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Melon;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        var collection = new ServiceCollection();
        collection.AddSingleton<MainViewModel>();
        collection.AddTransient<PlaybackControlsViewModel>();
        collection.AddTransient<LibraryCatalogViewModel>();
        collection.AddTransient<PlaylistCatalogViewModel>();
        collection.AddTransient<LibraryViewModel>();

        collection.AddSingleton<IMessenger, StrongReferenceMessenger>();
        collection.AddSingleton<IPlayerService, NAudioPlayerService>();

        collection.AddSingleton<PageFactory>();
        collection.AddSingleton<Func<ApplicationPageNames, PageViewModel>>(x => name => name switch
        {
            ApplicationPageNames.Library => x.GetRequiredService<LibraryCatalogViewModel>(),
            ApplicationPageNames.Playlists => x.GetRequiredService<PlaylistCatalogViewModel>(),
            _ => throw new ArgumentException("Invalid page name", nameof(name))
        });

        collection.AddSingleton<LibraryFactory>();
        collection.AddSingleton<Func<LibraryViewModel>>(x => () => x.GetRequiredService<LibraryViewModel>());

        var services = collection.BuildServiceProvider();
        
        var vm = services.GetRequiredService<MainViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
