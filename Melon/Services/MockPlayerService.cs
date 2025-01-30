using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Data;
using Melon.Models;

namespace Melon.Services
{
    public class MockPlayerService : IPlayerService
    {
        public float MaxGain => 1.0f;

        public float MinGain => 0.0f;

        public float Gain { get; set; }
        public PlaybackState State => PlaybackState.Stopped;
        public Song? CurrentSong { get; set ; }

        public void Pause() { }

        public void Play() { }

        public void Stop() { }
    }
}
