using Melon.Features.Playback.PlaybackControlsBar;
using Microsoft.Extensions.DependencyInjection;

namespace Melon.Features.Playback
{
    public static class DependencyInjection
    {
        public static void AddPlaybackFeature(this IServiceCollection services)
        {
            services.AddSingleton<IPlayerService, NAudioPlayerService>();

            services.AddSingleton<PlaybackControlsBarViewModel>();
        }
    }
}
