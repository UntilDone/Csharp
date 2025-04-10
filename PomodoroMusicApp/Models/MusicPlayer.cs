using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace PomodoroMusicApp.Models
{
    public class MusicPlayer
    {
        private readonly MediaPlayer _mediaPlayer;
        private List<string> _musicFiles;
        private int _currentTrackIndex = 0;

        public bool IsPlaying { get; private set; }

        public MusicPlayer(string musicDirectory)
        {
            _mediaPlayer = new MediaPlayer();
            LoadMusicFiles(musicDirectory);
        }

        private void LoadMusicFiles(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new DirectoryNotFoundException("Music directory not found.");
            }

            _musicFiles = Directory.GetFiles(directory, "*.mp3").ToList();

            if (!_musicFiles.Any())
            {
                throw new FileNotFoundException("No music files found in the directory.");
            }
        }

        public void Play()
        {
            if (!_musicFiles.Any())
                return;

            var currentFile = _musicFiles[_currentTrackIndex];
            _mediaPlayer.Open(new Uri(currentFile));
            _mediaPlayer.Play();
            IsPlaying = true;

            _mediaPlayer.MediaEnded += (s, e) => PlayNext();
        }

        public void Pause()
        {
            _mediaPlayer.Pause();
            IsPlaying = false;
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
            IsPlaying = false;
        }

        private void PlayNext()
        {
            _currentTrackIndex = (_currentTrackIndex + 1) % _musicFiles.Count;
            Play();
        }
    }
}
