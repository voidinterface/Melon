using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Features.Songs;

namespace Melon.Features.Playback
{
    public interface IPlayerService
    {
        float MaxGain { get; }
        float MinGain { get; }
        public float Gain { get; set; }
        public PlaybackState State { get; }
        public Song? CurrentSong { get; set; }
        public TimeSpan Position { get; set; }
        public TimeSpan TotalDuration { get; }

        public event EventHandler<PlaybackState> PlaybackStateChanged;

        public void Stop();
        public void Pause();
        public void Play(string path);

        public void Resume();
    }
}
