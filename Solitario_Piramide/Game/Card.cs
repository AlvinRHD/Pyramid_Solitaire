using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitario_Piramide.Game
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
