using Solitario_Piramide.Interfaces;
using System.Media;
using static Solitario_Piramide.Sound.SoundManager;

namespace Solitario_Piramide.Sound
{

    public class SoundManager : ISoundManager, IDisposable
    {
        private static readonly SoundPlayer backgroundMusic = new SoundPlayer(@"C:\Users\ezequ\OneDrive\Escritorio\Pyramid_Solitaire\Solitario_Piramide\Sounds\1.wav");
        private static readonly SoundPlayer cardFlipSound = new SoundPlayer(@"C:\Users\ezequ\OneDrive\Escritorio\Pyramid_Solitaire\Solitario_Piramide\Sounds\2.wav");

        public void PlayBackgroundMusic()
        {
            try
            {
                backgroundMusic.PlayLooping();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reproducir música de fondo: " + ex.Message);
            }
        }

        public void PlayCardFlipSound()
        {
            try
            {
                cardFlipSound.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reproducir el sonido de volteo de carta: " + ex.Message);
            }
        }

        public void Dispose()
        {
            backgroundMusic.Dispose();
            cardFlipSound.Dispose();
        }
    }
}

