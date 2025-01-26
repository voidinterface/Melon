using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Melon.Services
{
    public class NAudioPlayerService : IPlayerService
    {
        private readonly WaveOutEvent outputDevice = new WaveOutEvent();
        private AudioFileReader? audioFile;


        public NAudioPlayerService()
        {
            
        }
        public void playSong(string path)
        {
            if (path == null) return;

            audioFile = new AudioFileReader(path);
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }

        public void stop()
        {
            outputDevice.Stop();
        }
    }
}
