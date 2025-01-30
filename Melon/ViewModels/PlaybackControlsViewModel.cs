using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Melon.Data;
using Melon.Services;

namespace Melon.ViewModels
{
    public partial class PlaybackControlsViewModel : ViewModelBase
    {
        readonly IPlayerService _playerService;
        readonly IMessenger _messenger;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PlayCommand))]
        [NotifyCanExecuteChangedFor(nameof(PauseCommand))]
        [NotifyPropertyChangedFor(nameof(IsPlayButtonVisible))]
        [NotifyPropertyChangedFor(nameof(IsPauseButtonVisible))]
        private PlaybackState _state;

        public float MaxGain => _playerService.MaxGain;
        public float MinGain => _playerService.MinGain;

        [ObservableProperty]
        private float _gain;
        partial void OnGainChanged(float value)
        {
            if (value <= MaxGain && value >= MinGain)
            {
                _playerService.Gain = value;
            }
        }
        public PlaybackControlsViewModel()
        {
            if (Design.IsDesignMode)
            {
                _playerService = new MockPlayerService();
                _messenger = new WeakReferenceMessenger();

                Gain = _playerService.MaxGain;
                State = PlaybackState.Stopped;
            } else
            {
                throw new InvalidOperationException("This constructor should only be used in design mode.");
            }

        }

        public PlaybackControlsViewModel(IPlayerService playerService, IMessenger messenger)
        {
            _messenger = messenger;
            _playerService = playerService;

            Gain = _playerService.MaxGain;
            State = _playerService.State;

            _messenger.Register<PlaybackStateChangedMessage>(this, (recipient, message) =>
            {
                State = message.Value;
            });
        }

        private bool CanPlay() => State is PlaybackState.Paused;

        private bool CanPause() => State is PlaybackState.Playing;

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

        public bool IsPauseButtonVisible => State is PlaybackState.Playing;
        public bool IsPlayButtonVisible => !IsPauseButtonVisible;
    }
}
