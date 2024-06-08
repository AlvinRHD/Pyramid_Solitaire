using Solitario_Piramide.Interfaces;

namespace Solitario_Piramide.Game
{
    public class Card : ICard
    {
        public int Value { get; set; }
        public string Suit { get; set; }

        public Card(int value, string suit)
        {
            if (value < 1 || value > 13) throw new ArgumentException("Invalid card value");
            if (string.IsNullOrWhiteSpace(suit)) throw new ArgumentException("Invalid card suit");
            Value = value;
            Suit = suit;
        }
    }
}
