namespace PyramidSolitaire.Game
{
    public class Player
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
    }
}
