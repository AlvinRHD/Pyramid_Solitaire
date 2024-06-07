using Solitario_Piramide.Inferfaces;

namespace Solitario_Piramide.Game
{
    public class Card : ICard
    {
        public int Value { get; set; }
        public string Suit { get; set; }

        public Card(int value, string suit)
        {
            Value = value;
            Suit = suit;
        }
    }
}
