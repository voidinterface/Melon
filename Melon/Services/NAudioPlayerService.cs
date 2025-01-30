using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Data;
using Melon.Models;
using NAudio.Wave;

namespace Melon.Services
{
    public class NAudioPlayerService : IPlayerService
    {
        private readonly IMessenger _messenger;

        private readonly WaveOutEvent outputDevice = new();
        private AudioFileReader? audioFile;
        private SampleProviderWithGain? sampleProviderWithGain;

        private Data.PlaybackState _state;
        public Data.PlaybackState State => _state;

        private Song? _currentSong;
        public Song? CurrentSong
        {
            get => _currentSong;
            set
            {
                _currentSong = value;

                LoadCurrentSong();
            }
        }

        private float _gain;
        public float Gain
        {
            get => _gain;
            set
            {
                if (value > MaxGain || value < MinGain) throw new ArgumentOutOfRangeException($"Gain out of range: {value}");
                _gain = value;
            }
        }

        public float MaxGain => 1.0f;
        public float MinGain => 0.0f;

        public NAudioPlayerService(IMessenger messenger)
        {
            _gain = MaxGain;
            _messenger = messenger;
        }

        private void LoadCurrentSong()
        {
            _state = Data.PlaybackState.Stopped;
            _messenger.Send(new PlaybackStateChangedMessage(_state));

            outputDevice.Stop();
            audioFile?.Dispose();

            //TODO: Handle invalid file paths or null
            if (CurrentSong?.Path == null) return; 

            audioFile = new AudioFileReader(CurrentSong?.Path);

            sampleProviderWithGain = new SampleProviderWithGain(this, audioFile);
            outputDevice.Init(sampleProviderWithGain);

            _state = Data.PlaybackState.Paused;
            _messenger.Send(new PlaybackStateChangedMessage(_state));

            Play();
        }

        public void Stop()
        {
            outputDevice.Stop();
        }

        public void Pause()
        {
            if (_state == Data.PlaybackState.Playing)
            {
                outputDevice.Pause();
                _state = Data.PlaybackState.Paused;
                _messenger.Send(new PlaybackStateChangedMessage(_state));
            }
        }

        public void Play()
        {
            if (_state == Data.PlaybackState.Paused)
            {
                outputDevice.Play();
                _state = Data.PlaybackState.Playing;
                _messenger.Send(new PlaybackStateChangedMessage(_state));
            }
        }

        class SampleProviderWithGain : ISampleProvider
        {
            private readonly NAudioPlayerService _playerService;
            private readonly ISampleProvider _source;

            internal SampleProviderWithGain(NAudioPlayerService playerService, ISampleProvider source)
            {
                _playerService = playerService;
                _source = source;
            }

            public WaveFormat WaveFormat => _source.WaveFormat;

            public int Read(float[] buffer, int offset, int count)
            {
                int samplesRead = _source.Read(buffer, offset, count);

                if (_playerService.Gain != 1.0f)
                {
                    for (int n = 0; n < count; n++)
                    {
                        buffer[n + offset] *= _playerService.Gain;
                    }
                }

                return samplesRead;
            }
        }
    }
}
