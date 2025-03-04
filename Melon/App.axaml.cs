using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Melon.Database;
using Microsoft.EntityFrameworkCore;
using Melon.Pages;
using Melon.Features.Locations.WatchLocations;
using Melon.Features.Locations;
using Melon.Features.Playback;

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

        collection.AddLocationFeature();

        collection.AddPlaybackFeature();

        collection.AddPages();

        var services = collection.BuildServiceProvider();

        // Apply pending migrations at startup
        using (var scope = services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MelonDbContext>();
            dbContext.Database.Migrate();

            var locationWatcher = scope.ServiceProvider.GetRequiredService<ILocationWatcherService>();
            locationWatcher.Start();
        }

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
