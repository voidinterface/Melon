using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Features.Playback;

namespace Melon.Features.Playback.PlaybackControlsBar
{
    public partial class PlaybackControlsBarViewModel : ObservableObject
    {
        readonly IPlayerService _playerService;
        readonly DispatcherTimer _timer;

        private double _seekValue = TimeSpan.Zero.TotalSeconds;
        private bool _isSeeking = false;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PlayCommand))]
        [NotifyCanExecuteChangedFor(nameof(PauseCommand))]
        [NotifyPropertyChangedFor(nameof(IsSeekEnabled))]
        [NotifyPropertyChangedFor(nameof(IsPlayButtonVisible))]
        [NotifyPropertyChangedFor(nameof(IsPauseButtonVisible))]
        private PlaybackState _state;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PositionSeconds))]
        [NotifyPropertyChangedFor(nameof(PositionString))]
        private TimeSpan _position;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DurationString))]
        [NotifyPropertyChangedFor(nameof(SeekMaximum))]
        private TimeSpan _duration;

        public float MaxGain => _playerService.MaxGain;
        public float MinGain => _playerService.MinGain;

        public double PositionSeconds
        {
            get => Position.TotalSeconds;
            set
            {
                _seekValue = value;
                OnPropertyChanged(nameof(PositionString));
            }
        }

        public string PositionString =>
            _isSeeking 
                ? TimeSpan.FromSeconds(_seekValue).ToString(@"mm\:ss")
                : Position.ToString(@"mm\:ss");

        public string DurationString => Duration.ToString(@"mm\:ss");

        public static double SeekMinimum => TimeSpan.Zero.TotalSeconds;
        public double SeekMaximum => Duration.TotalSeconds;


        [ObservableProperty]
        private float _gain;
        partial void OnGainChanged(float value)
        {
            if (value <= MaxGain && value >= MinGain)
            {
                _playerService.Gain = value;
            }
        }

        public PlaybackControlsBarViewModel() : this(new MockPlayerService())
        {
            Debug.Assert(Design.IsDesignMode);
        }

        public PlaybackControlsBarViewModel(IPlayerService playerService)
        {
            _playerService = playerService;
            _timer = CreateDefaultSeekTimer();

            Gain = _playerService.MaxGain;
            State = _playerService.State;
            Position = _playerService.Position;
            Duration = _playerService.TotalDuration;

            _playerService.PlaybackStateChanged += (sender, state) => HandlePlaybackStateChanged(state);

            _timer.IsEnabled = State is PlaybackState.Playing;
        }

        private DispatcherTimer CreateDefaultSeekTimer()
        {
            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += (object? sender, EventArgs e) =>
            {
                if (!_isSeeking)
                {
                    Position = _playerService.Position;
                }
                
            };
            return timer;
        }

        private void HandlePlaybackStateChanged(PlaybackState state)
        {
            State = state;
            Position = _playerService.Position;
            Duration = _playerService.TotalDuration;

            _timer.IsEnabled = State is PlaybackState.Playing;
        }

        private bool CanPlay() => State is PlaybackState.Paused;

        private bool CanPause() => State is PlaybackState.Playing;

        [RelayCommand]
        public void SeekStarted() => _isSeeking = true;

        [RelayCommand]
        public void SeekCompleted()
        {
            _isSeeking = false;
            _playerService.Position = TimeSpan.FromSeconds(_seekValue);
        }


        [RelayCommand (CanExecute = nameof(CanPause))]
        public void Pause()
        {
            _playerService.Pause();
        }

        [RelayCommand(CanExecute = nameof(CanPlay))]
        public void Play()
        {
            _playerService.Play();
        }

        public bool IsSeekEnabled => State is PlaybackState.Playing or PlaybackState.Paused;
        public bool IsPauseButtonVisible => State is PlaybackState.Playing;
        public bool IsPlayButtonVisible => !IsPauseButtonVisible;
    }
}
