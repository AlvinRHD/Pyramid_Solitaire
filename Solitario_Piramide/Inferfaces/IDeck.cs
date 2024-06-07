using Solitario_Piramide.Game;

namespace Solitario_Piramide.Inferfaces
{
    public interface IDeck
    {
        Card DrawCard();
        List<Card> DrawCards(int count);
        void Shuffle();
        void ResetDeck(); // Nuevo método para reiniciar el mazo
        int CardsRemaining { get; } // Propiedad para obtener el número de cartas restantes
    }
}