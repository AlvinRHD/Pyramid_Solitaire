using Solitario_Piramide.Game;
using System;
using System.Collections.Generic;

namespace PyramidSolitaire.Game
{
    public class Deck
    {
        private Stack<Card> cards;

        public Deck()
        {
            cards = new Stack<Card>();
            InitializeDeck();
            Shuffle();
        }

        private void InitializeDeck()
        {
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            for (int i = 1; i <= 13; i++)
            {
                foreach (string suit in suits)
                {
                    cards.Push(new Card(i, suit));
                }
            }
        }

        private void Shuffle()
        {
            var rnd = new Random();
            var shuffledCards = new List<Card>(cards);
            for (int i = shuffledCards.Count - 1; i > 0; i--)
            {
                int swapIndex = rnd.Next(i + 1);
                Card temp = shuffledCards[i];
                shuffledCards[i] = shuffledCards[swapIndex];
                shuffledCards[swapIndex] = temp;
            }
            cards = new Stack<Card>(shuffledCards);
        }

        public Card DrawCard()
        {
            return cards.Pop();
        }
    }
}
