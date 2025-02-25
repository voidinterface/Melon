using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Features.Playback;
using Melon.Features.Main;
using Microsoft.Extensions.DependencyInjection;
using Melon.Features.Libraries;
using Melon.Database;
using Microsoft.EntityFrameworkCore;

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

        collection.AddDbContext<MelonDbContext>(options =>
            options.UseSqlite("Data Source=melon.db"));

        collection.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(App).Assembly);
        });

        collection.AddLibraryFeature();

        collection.AddPlaybackFeature();

        collection.AddMainFeature();

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
