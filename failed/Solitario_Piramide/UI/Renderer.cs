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
│                                                                          │
│              ( D )                                 ( B )                 │ 
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




        public void RenderPyramid(IPyramid pyramid, int score, (int, int)? selectedCardPosition)
        {
            string[] lines = background.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            int startRow = 5; // Starting row for the pyramid in the background
            int startCol = 32; // Starting column for the pyramid in the background

            for (int i = 0; i < pyramid.Rows.Count; i++)
            {
                for (int j = 0; j < pyramid.Rows[i].Count; j++)
                {
                    string cardString = GetCardString(pyramid.Rows[i][j]);
                    int colOffset = (7 - i) + j * 6;
                    lines[startRow + i] = lines[startRow + i].Remove(startCol + colOffset, 2).Insert(startCol + colOffset, cardString);
                }
            }

            // Highlight the selected card if it's provided
            if (selectedCardPosition != null)
            {
                int row = selectedCardPosition.Value.Item1;
                int col = selectedCardPosition.Value.Item2;
                int colOffset = (7 - row) + col * 6;
                lines[startRow + row] = lines[startRow + row].Remove(startCol + colOffset, 2).Insert(startCol + colOffset, "[bold red]XX[/]");
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
            return value switch
            {
                1 => "A",
                11 => "J",
                12 => "Q",
                13 => "K",
                _ => value.ToString()
            };
        }

        private static string GetSuitString(string suit)
        {
            return suit switch
            {
                "Hearts" => "♥",
                "Diamonds" => "♦",
                "Clubs" => "♣",
                "Spades" => "♠",
                _ => "?"
            };
        }
    }
}
