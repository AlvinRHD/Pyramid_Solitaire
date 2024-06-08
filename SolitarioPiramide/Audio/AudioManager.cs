using System;
using System.Threading.Tasks;
using NAudio.Wave;

namespace SolitarioPiramide.Audio
{
    public static class AudioManager
    {
        private static IWavePlayer backgroundPlayer;
        private static AudioFileReader backgroundReader;
        private static WaveOutEvent actionPlayer;
        private static AudioFileReader actionReader;

        static AudioManager()
        {
            InitializeBackgroundPlayer();
            InitializeActionPlayer();

            actionPlayer.Volume = 1.0f;
        }

        private static void InitializeBackgroundPlayer()
        {
            backgroundPlayer = new WaveOutEvent();
            backgroundReader = new AudioFileReader("C:\\Users\\jorge\\Desktop\\Pyramid_Solitaire\\SolitarioPiramide\\Audio\\BackGround.wav");
            backgroundPlayer.Init(backgroundReader);
            backgroundPlayer.PlaybackStopped += BackgroundPlayer_PlaybackStopped;
        }

        private static void InitializeActionPlayer()
        {
            actionPlayer = new WaveOutEvent();
            actionReader = new AudioFileReader("C:\\Users\\jorge\\Desktop\\Pyramid_Solitaire\\SolitarioPiramide\\Audio\\Cards.wav");
            actionPlayer.Init(actionReader);
        }

        private static void BackgroundPlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            backgroundReader.Position = 0;
            backgroundPlayer.Play();
        }

        public static void PlayBackgroundMusic()
        {
            backgroundPlayer.Play();
        }

        public static void PlayActionSound()
        {
            actionReader.Position = 0;
            actionPlayer.Play();
        }

        public static void ResumeBackgroundMusic()
        {
            if (backgroundPlayer.PlaybackState != PlaybackState.Playing)
            {
                backgroundPlayer.Play();
            }
        }

        public static void PlayActionSoundWithBackground()
        {
            PlayActionSound();
            Task.Delay(500).ContinueWith(t => ResumeBackgroundMusic());
        }

        public static void PlayScoringSound()
        {
            PlayActionSound();
        }
    }
}
