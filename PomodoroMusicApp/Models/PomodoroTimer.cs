using System.Timers;

namespace PomodoroMusicApp.Models
{
    public class PomodoroTimer
    {
        private System.Timers.Timer _timer;
        private TimeSpan _remainingTime;

        public TimeSpan RemainingTime => _remainingTime;
        public bool IsRunning { get; private set; }
        public bool IsWorkSession { get; private set; } = true;

        public event Action<TimeSpan> TimeChanged;
        public event Action SessionEnded;

        public PomodoroTimer()
        {
            _timer = new System.Timers.Timer(1000); // 1초마다 Tick
            _timer.Elapsed += OnTimerElapsed;
            _remainingTime = TimeSpan.FromMinutes(25);
        }

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            _timer.Start();
        }

        public void Pause()
        {
            if (!IsRunning) return;
            IsRunning = false;
            _timer.Stop();
        }

        public void Reset()
        {
            _timer.Stop();
            IsRunning = false;
            _remainingTime = IsWorkSession ? TimeSpan.FromMinutes(25) : TimeSpan.FromMinutes(5);
            TimeChanged?.Invoke(_remainingTime);
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_remainingTime.TotalSeconds > 0)
            {
                _remainingTime = _remainingTime.Subtract(TimeSpan.FromSeconds(1));
                TimeChanged?.Invoke(_remainingTime);
            }
            else
            {
                _timer.Stop();
                IsRunning = false;
                IsWorkSession = !IsWorkSession;
                SessionEnded?.Invoke();
            }
        }
    }
}
