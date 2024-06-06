using System.Collections.Generic;

namespace PyramidSolitaire.Game
{
    public class Pyramid
    {
        public List<Card[]> Rows { get; }

        public Pyramid(Deck deck)
        {
            Rows = new List<Card[]>();
            for (int i = 1; i <= 7; i++)
            {
                Card[] row = new Card[i];
                for (int j = 0; j < i; j++)
                {
                    row[j] = deck.DrawCard();
                    row[j].IsFaceUp = true;
                }
                Rows.Add(row);
            }
        }

        public void RemoveCard(Card card)
        {
            foreach (var row in Rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] == card)
                    {
                        row[i] = null;
                    }
                }
            }
        }

        public bool AllCardsRemoved()
        {
            foreach (var row in Rows)
            {
                foreach (var card in row)
                {
                    if (card != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
