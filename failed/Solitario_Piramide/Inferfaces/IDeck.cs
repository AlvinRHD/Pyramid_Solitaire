using Solitario_Piramide.Game;
using System.Collections.Generic;

namespace Solitario_Piramide.Interfaces
{
    public interface IDeck
    {
        Card DrawCard();
        List<Card> DrawCards(int count);
        void Shuffle();
        void ResetDeck();
        int CardsRemaining { get; }
    }
}
