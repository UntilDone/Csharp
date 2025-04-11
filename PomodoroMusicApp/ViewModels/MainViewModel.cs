using PomodoroMusicApp.Models;
using PomodoroMusicApp.Helpers;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PomodoroMusicApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly PomodoroTimer _pomodoroTimer;
        private readonly MusicPlayer _musicPlayer;

        private string _timeDisplay;
        public string TimeDisplay
        {
            get => _timeDisplay;
            set
            {
                _timeDisplay = value;
                OnPropertyChanged();
            }
        }

        private string _sessionType;
        public string SessionType
        {
            get => _sessionType;
            set
            {
                _sessionType = value;
                OnPropertyChanged();
            }
        }

        public ICommand StartCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ResetCommand { get; }

        public MainViewModel()
        {
            string musicFolderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Music");
            _pomodoroTimer = new PomodoroTimer();
            _musicPlayer = new MusicPlayer(musicFolderPath); // Music 폴더 경로

            _pomodoroTimer.TimeChanged += OnTimeChanged;
            _pomodoroTimer.SessionEnded += OnSessionEnded;

            StartCommand = new RelayCommand(StartTimer);
            PauseCommand = new RelayCommand(PauseTimer);
            ResetCommand = new RelayCommand(ResetTimer);

            TimeDisplay = FormatTime(_pomodoroTimer.RemainingTime);
            SessionType = "Work Session";
        }

        private void StartTimer()
        {
            _pomodoroTimer.Start();
            _musicPlayer.Play();
        }

        private void PauseTimer()
        {
            _pomodoroTimer.Pause();
            _musicPlayer.Pause();
        }

        private void ResetTimer()
        {
            _pomodoroTimer.Reset();
            _musicPlayer.Stop();
            TimeDisplay = FormatTime(_pomodoroTimer.RemainingTime);
            SessionType = _pomodoroTimer.IsWorkSession ? "Work Session" : "Break";
        }

        private void OnTimeChanged(TimeSpan remainingTime)
        {
            TimeDisplay = FormatTime(remainingTime);
        }

        private void OnSessionEnded()
        {
            _musicPlayer.Stop();
            _pomodoroTimer.Reset();

            SessionType = _pomodoroTimer.IsWorkSession ? "Work Session" : "Break";
            TimeDisplay = FormatTime(_pomodoroTimer.RemainingTime);

            // 자동으로 다음 세션 시작
            _pomodoroTimer.Start();
            _musicPlayer.Play();
        }

        private string FormatTime(TimeSpan time)
        {
            return time.ToString(@"mm\:ss");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

