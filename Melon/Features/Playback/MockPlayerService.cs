using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Features.Songs;

namespace Melon.Features.Playback
{
    public class MockPlayerService : IPlayerService
    {
        public float MaxGain => 1.0f;

        public float MinGain => 0.0f;

        public float Gain { get; set; }
        public PlaybackState State => PlaybackState.Stopped;
        public Song? CurrentSong { get; set; }
        public TimeSpan Position { get; set; } = TimeSpan.Zero;

        public TimeSpan TotalDuration => TimeSpan.FromMinutes(3d);

        public event EventHandler<PlaybackState>? PlaybackStateChanged;

        public void Pause() { }

        public void Play(string path) { }

        public void Stop() { }

        public void Resume() { }
    }
}
