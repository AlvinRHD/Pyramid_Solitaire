using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitarioPiramide.Game
{
    public class Deck
    {
        private List<Card> cards;
        private static readonly Random random = new Random();

        public Deck()
        {
            cards = new List<Card>();
            string[] suits = { "♠", "♥", "♦", "♣" };
            string[] ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

            foreach (string suit in suits)
            {
                for (int i = 0; i < ranks.Length; i++)
                {
                    cards.Add(new Card(suit, ranks[i], values[i]));
                }
            }
        }

        public void Shuffle()
        {
            for (int i = cards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Card temp = cards[i];
                cards[i] = cards[j];
                cards[j] = temp;
            }
        }

        public Card Deal()
        {
            if (cards.Count == 0)
                return null;
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }
}
