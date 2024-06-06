namespace PyramidSolitaire.Game
{
    public class Card
    {
        public int Value { get; }
        public string Suit { get; }
        public bool IsFaceUp { get; set; }

        public Card(int value, string suit)
        {
            Value = value;
            Suit = suit;
            IsFaceUp = false;
        }
    }
}
