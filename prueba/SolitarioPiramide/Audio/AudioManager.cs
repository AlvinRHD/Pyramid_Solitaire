using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SolitarioPiramide.Audio
{
    public static class AudioManager
    {
        private static SoundPlayer backgroundPlayer;
        private static SoundPlayer actionPlayer;

        static AudioManager()
        {
            backgroundPlayer = new SoundPlayer("C:\\Users\\ezequ\\OneDrive\\Escritorio\\Pyramid_Solitaire\\prueba\\SolitarioPiramide\\Audio\\BackGround.wav");
            actionPlayer = new SoundPlayer("C:\\Users\\ezequ\\OneDrive\\Escritorio\\Pyramid_Solitaire\\prueba\\SolitarioPiramide\\Audio\\Cards.wav");
        }

        public static void PlayBackgroundMusic()
        {
            backgroundPlayer.PlayLooping();
        }

        public static void PlayActionSound()
        {
            actionPlayer.Play();
        }
    }
}
