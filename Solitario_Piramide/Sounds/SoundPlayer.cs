using System.Media;

namespace Solitario_Piramide.Sound
{
    public static class SoundPlayer
    {
        private static readonly SoundPlayer backgroundMusic = new SoundPlayer("Sounds/Background.wav");
        private static readonly SoundPlayer cardFlipSound = new SoundPlayer("Sounds/CardsPareja.wav");

        public static void PlayBackgroundMusic()
        {
            backgroundMusic.PlayLooping();
        }

        public static void PlayCardFlipSound()
        {
            cardFlipSound.Play();
        }
    }
}
