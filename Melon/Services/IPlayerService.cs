using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Melon.Data;
using Melon.Models;

namespace Melon.Services
{
    public interface IPlayerService
    {
        float MaxGain { get;  }
        float MinGain { get; }
        public float Gain { get; set; }
        public PlaybackState State { get; }
        public Song? CurrentSong { get; set; }

        public void Stop();
        public void Pause();
        public void Play();
    }
}
