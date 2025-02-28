using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Pages.Settings;
using Melon.Pages.Sidebar;
using Microsoft.Extensions.DependencyInjection;

namespace Melon.Pages
{
    public static class DependencyInjection
    {
        public static void AddPages(this IServiceCollection services)
        {
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<SidebarViewModel>();
            services.AddSingleton<MainViewModel>();
        }
    }
}
