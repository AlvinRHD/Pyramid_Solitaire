using System.Media;

namespace PyramidSolitaire.Sounds
{
    public static class SoundPlayer
    {
        private static readonly SoundPlayer backgroundMusic = new SoundPlayer("Sounds/ColorFul-Flowers(chosic.com).mp3");
        private static readonly SoundPlayer cardFlipSound = new SoundPlayer("Sounds/gameboy.wav");

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
