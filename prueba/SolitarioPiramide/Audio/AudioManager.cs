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
            backgroundPlayer = new SoundPlayer("C:\\Desktop\\Escritorio 2024\\POO\\Solitario\\prueba\\SolitarioPiramide\\Audio\\1.wav");
            actionPlayer = new SoundPlayer("C:\\Desktop\\Escritorio 2024\\POO\\Solitario\\prueba\\SolitarioPiramide\\Audio\\2.wav");
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
