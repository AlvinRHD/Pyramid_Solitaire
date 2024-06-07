using SolitarioPiramide.Audio;
using SolitarioPiramide.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolitarioPiramide.Game
{
    public static class GameManager
    {
        private static int score = 0;
        private static Deck deck;
        private static Card[][] pyramid;
        private static List<Card> stock;
        private static List<Card> waste;

        public static void StartNewGame()
        {
            Console.Clear();
            AnsiConsole.Markup("[bold yellow]Iniciando un nuevo juego...[/]\n");
            PlayBackgroundMusic();
            score = 0;
            InitializeDeck();
            DealCards();
            PlayGame();
        }

        private static void InitializeDeck()
        {
            deck = new Deck();
            deck.Shuffle();
            pyramid = new Card[7][];
            stock = new List<Card>();
            waste = new List<Card>();
        }

        private static void DealCards()
        {
            for (int row = 0; row < 7; row++)
            {
                pyramid[row] = new Card[row + 1];
                for (int col = 0; col < row + 1; col++)
                {
                    pyramid[row][col] = deck.Deal();
                    pyramid[row][col].IsFaceUp = (row == 6); // Las cartas de la base están boca arriba
                }
            }

            Card card;
            while ((card = deck.Deal()) != null)
            {
                stock.Add(card);
            }
        }

        private static void PlayGame()
        {
            bool gameRunning = true;

            while (gameRunning)
            {
                Console.Clear();
                DisplayGameBoard();
                DisplayCards();

                var input = AnsiConsole.Ask<string>("Seleccione dos cartas que sumen 13 (formato: fila1,col1 fila2,col2) o 's' para sacar una carta del stock:");

                if (input.ToLower() == "s")
                {
                    DrawFromStock();
                }
                else
                {
                    var selections = input.Split(' ');

                    if (selections.Length == 2)
                    {
                        var card1 = selections[0].Split(',');
                        var card2 = selections[1].Split(',');

                        if (card1.Length == 2 && card2.Length == 2 &&
                            int.TryParse(card1[0], out int row1) &&
                            int.TryParse(card1[1], out int col1) &&
                            int.TryParse(card2[0], out int row2) &&
                            int.TryParse(card2[1], out int col2))
                        {
                            if (IsValidSelection(row1, col1, row2, col2))
                            {
                                UpdateBoard(row1, col1, row2, col2);
                                PlayActionSound();
                            }
                            else
                            {
                                AnsiConsole.Markup("[bold red]Selección inválida, intenta de nuevo.[/]\n");
                            }
                        }
                    }
                }

                gameRunning = !IsGameOver();
            }

            EndGame();
        }

        private static void DrawFromStock()
        {
            if (stock.Count > 0)
            {
                Card drawnCard = stock[0];
                stock.RemoveAt(0);
                waste.Add(drawnCard);
                AnsiConsole.Markup($"[bold yellow]Sacaste una carta del stock: {drawnCard}[/]\n");
            }
            else
            {
                AnsiConsole.Markup("[bold red]No hay más cartas en el stock.[/]\n");
            }
        }

        private static bool IsValidSelection(int row1, int col1, int row2, int col2)
        {
            Card card1 = pyramid[row1][col1];
            Card card2 = pyramid[row2][col2];

            if (!card1.IsFaceUp || !card2.IsFaceUp || card1 == card2)
                return false;

            return (card1.Value + card2.Value == 13);
        }

        private static void UpdateBoard(int row1, int col1, int row2, int col2)
        {
            pyramid[row1][col1] = null;
            pyramid[row2][col2] = null;
            score += 10;

            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < row + 1; col++)
                {
                    if (pyramid[row][col] != null && !pyramid[row][col].IsFaceUp)
                    {
                        if ((row == 6) ||
                            (row < 6 && pyramid[row + 1][col] == null && pyramid[row + 1][col + 1] == null))
                        {
                            pyramid[row][col].IsFaceUp = true;
                        }
                    }
                }
            }
        }

        private static bool IsGameOver()
        {
            for (int row = 0; row < 7; row++)
            {
                for (int col = 0; col < row + 1; col++)
                {
                    if (pyramid[row][col] != null && pyramid[row][col].IsFaceUp)
                    {
                        for (int r = row; r < 7; r++)
                        {
                            for (int c = (r == row ? col + 1 : 0); c < r + 1; c++)
                            {
                                if (pyramid[r][c] != null && pyramid[r][c].IsFaceUp)
                                {
                                    if (pyramid[row][col].Value + pyramid[r][c].Value == 13)
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return stock.Count == 0 && waste.Count == 0;
        }

        private static void EndGame()
        {
            AnsiConsole.Markup("[bold yellow]El juego ha terminado![/]\n");
            AnsiConsole.Markup($"[bold green]Puntuación: {score}[/]\n");

            if (AnsiConsole.Confirm("¿Quieres jugar de nuevo?"))
            {
                StartNewGame();
            }
            else
            {
                Menu.DisplayMainMenu();
            }
        }

        private static void PlayBackgroundMusic()
        {
            AudioManager.PlayBackgroundMusic();
        }

        private static void PlayActionSound()
        {
            AudioManager.PlayActionSound();
        }

        private static void DisplayGameBoard()
        {
            Console.Clear();
            AnsiConsole.Write(
            """
            ┌──────────────────────────────────────────────────────────────────────────┐
            │                                                                          │
            │                               SOLITARIO                                  │
            │                                                                          │
            ├──────────────────────────────────────────────────────────────────────────┤
            │
            """);

            for (int row = 0; row < 7; row++)
            {
                AnsiConsole.Write("│");
                for (int i = 0; i < 7 - row; i++)
                {
                    AnsiConsole.Write("   ");
                }
                for (int col = 0; col < row + 1; col++)
                {
                    AnsiConsole.Write(pyramid[row][col] != null ? pyramid[row][col].ToString() : "[  ]");
                    AnsiConsole.Write(" ");
                }
                AnsiConsole.Write(" │\n");
            }

            AnsiConsole.Write(
            """
            │                                                                          │
            │                     ( D )  ( C )                 ( B )                   │
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
            │   el                                           Rey (K): 13 puntos        │
            │                                                                          │
            └──────────────────────────────────────────────────────────────────────────┘
            """);
        }

        private static void DisplayCards()
        {
            for (int row = 0; row < 7; row++)
            {
                string rowStr = "";
                for (int col = 0; col < row + 1; col++)
                {
                    rowStr += pyramid[row][col] != null ? pyramid[row][col].ToString() : "[  ]";
                    rowStr += " ";
                }
                AnsiConsole.WriteLine(rowStr);
            }
            AnsiConsole.WriteLine($"\nStock: {stock.Count}");
            AnsiConsole.WriteLine($"Waste: {waste.Count}");
        }
    }
}
