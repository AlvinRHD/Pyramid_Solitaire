using Solitario_Piramide.Interfaces;

namespace Solitario_Piramide
{
    public class Player : IPlayer
    {
        public int Score { get; private set; }

        public Player()
        {
            Score = 0;
        }

        public void AddPoints(int points)
        {
            Score += points;
        }

        public void ResetScore()
        {
            Score = 0;
        }
    }
}
