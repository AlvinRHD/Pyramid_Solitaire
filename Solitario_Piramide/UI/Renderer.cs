using Spectre.Console;
using Solitario_Piramide.Game;
using Solitario_Piramide.Interfaces;

namespace Solitario_Piramide.UI
{
    public class Renderer : IRenderer
    {
        private static readonly string background = @"
┌──────────────────────────────────────────────────────────────────────────┐ 
│                                                                          │ 
│                               SOLITARIO                                  │ 
│                                                                          │ 
├──────────────────────────────────────────────────────────────────────────┤ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
│                                                                          │ 
├──────────────────────────────────────────────────────────────────────────┤ 
│                                                                          │ 
│                             SOLITARIO PIRAMIDE                           │ 
│                                                                          │ 
│                                                                          │ 
│      TIPO DE JUEGO : PIRÁMIDE    |   ESTADISTICAS DE VICTORIA 0.6%       │ 
│                                                                          │ 
├──────────────────────────────────────────────────────────────────────────┤ 
│     Intrucciones:                              Valor de las Cartas:      │ 
│                                                                          │ 
│   Se crea un mazo aleatorio, o una piramide    As: 1 punto               │ 
│   de cartas, intenta juntar 2 cartas que sus   2 a 10: su valor numérico │ 
│   valores sumen un total de 13 hasta que se    Jota (J): 11 puntos       │ 
│   termine la piramide y podras ganar,          Reina (Q): 12 puntos      │ 
│   el rey cuenta como 13, por lo tanto          Rey (K): 13 puntos        │ 
│   se puede tomar esa carta sola.                                         │ 
└──────────────────────────────────────────────────────────────────────────┘";

        public void RenderPyramid(IPyramid pyramid, int score)
        {
            string[] lines = background.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            int startRow = 5; // Starting row for the pyramid in the background
            int startCol = 32; // Starting column for the pyramid in the background

            for (int i = 0; i < pyramid.Rows.Count; i++)
            {
                for (int j = 0; j < pyramid.Rows[i].Count; j++)
                {
                    string cardString = pyramid.Rows[i][j] != null ? GetCardString(pyramid.Rows[i][j]) : "  ";
                    int colOffset = (7 - i) + j * 6;
                    lines[startRow + i] = lines[startRow + i].Remove(startCol + colOffset, 2).Insert(startCol + colOffset, cardString);
                }
            }

            Console.Clear();
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine($"Score: {score}");
        }

        private static string GetCardString(ICard card)
        {
            string valueString = GetValueString(card.Value);
            string suitString = GetSuitString(card.Suit);
            return $"{valueString}{suitString}";
        }

        private static string GetValueString(int value)
        {
            switch (value)
            {
                case 1: return "A";
                case 11: return "J";
                case 12: return "Q";
                case 13: return "K";
                default: return value.ToString();
            }
        }

        private static string GetSuitString(string suit)
        {
            switch (suit)
            {
                case "Hearts": return "♥";
                case "Diamonds": return "♦";
                case "Clubs": return "♣";
                case "Spades": return "♠";
                default: return "?";
            }
        }
    }
}
