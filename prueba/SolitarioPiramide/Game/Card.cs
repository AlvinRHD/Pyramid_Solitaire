using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitarioPiramide.Game
{
    public class Card
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public int Value { get; set; }
        public bool IsFaceUp { get; set; }

        public Card(string suit, string rank, int value)
        {
            Suit = suit;
            Rank = rank;
            Value = value;
            IsFaceUp = false;
        }

        public override string ToString()
        {
            return IsFaceUp ? $"[{Rank}{Suit}]" : "[  ]";
        }
    }
}
