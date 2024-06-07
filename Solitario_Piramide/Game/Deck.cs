using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitario_Piramide.Inferfaces;

namespace Solitario_Piramide.Game
{
    public class Deck : IDeck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();
            InitializeDeck();
        }

        private void InitializeDeck()
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            for (int i = 1; i <= 13; i++)
            {
                foreach (var suit in suits)
                {
                    cards.Add(new Card(i, suit));
                }
            }
        }

        public void Shuffle()
        {
            Random rng = new Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public Card DrawCard()
        {
            if (cards.Count > 0)
            {
                Card card = cards[0];
                cards.RemoveAt(0);
                return card;
            }
            throw new InvalidOperationException("The deck is empty");
        }

        public List<Card> DrawCards(int count)
        {
            if (count > cards.Count) throw new InvalidOperationException("Not enough cards to draw");
            List<Card> drawnCards = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                drawnCards.Add(DrawCard());
            }
            return drawnCards;
        }

        public void ResetDeck()
        {
            cards.Clear();
            InitializeDeck();
        }

        public int CardsRemaining => cards.Count;
    }
}
