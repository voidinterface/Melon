using Microsoft.Extensions.DependencyInjection;

namespace Melon.Features.Main
{
    public static class DependencyInjection
    {
        public static void AddMainFeature(this IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
        }
    }
}
