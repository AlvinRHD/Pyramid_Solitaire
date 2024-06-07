using System.Media;
using static Solitario_Piramide.Sound.SoundManager;

namespace Solitario_Piramide.Sound
{

    public class SoundManager : ISoundManager
    {
        private static readonly SoundPlayer backgroundMusic = new SoundPlayer(@"C:\Users\ezequ\OneDrive\Escritorio\Pyramid_Solitaire\Solitario_Piramide\Sounds\1.wav");
        private static readonly SoundPlayer cardFlipSound = new SoundPlayer(@"C:\Users\ezequ\OneDrive\Escritorio\Pyramid_Solitaire\Solitario_Piramide\Sounds\2.wav");

        public void PlayBackgroundMusic()
        {
            backgroundMusic.PlayLooping();
        }

        public void PlayCardFlipSound()
        {
            cardFlipSound.Play();
        }
    }
}

