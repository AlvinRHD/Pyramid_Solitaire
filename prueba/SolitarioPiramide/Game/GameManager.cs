using SolitarioPiramide.Audio;
using SolitarioPiramide.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;

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
            Card stockCard = null;

            while (gameRunning)
            {
                DisplayGameBoard(stockCard); // Mostrar el tablero actual con la carta del stock

                var input = AnsiConsole.Ask<string>("Seleccione dos cartas que sumen 13 (formato: fila1,col1 fila2,col2), " +
                    "'s' para sacar una carta del stock, " +
                    "'w' para usar la carta seleccionada del stock y si la 'k' sale en el stock se elimina automaticamente al seleccionar w" +
                    ", 'k' para eliminar un Rey de la piramide:");

                if (input.ToLower() == "s")
                {
                    DrawFromStock();
                    if (waste.Count > 0)
                    {
                        stockCard = waste[waste.Count - 1]; // Obtener la última carta del waste
                        Console.WriteLine($"stockCard after drawing: {stockCard.ToString()}"); // Depuración
                    }
                    else
                    {
                        stockCard = null;
                    }
                }
                else if (input.ToLower() == "w")
                {
                    if (stockCard != null)
                    {
                        if (stockCard.Value == 13) // Verificar si la carta del stock es un Rey
                        {
                            AnsiConsole.Markup($"[bold yellow]Eliminando la carta K del stock: {EscapeMarkup(stockCard.ToString())}[/]\n");
                            waste.Remove(stockCard);
                            stockCard = null;
                            PlayActionSound();
                        }
                        else
                        {
                            AnsiConsole.Markup($"[bold yellow]Estás usando la carta del stock: {EscapeMarkup(stockCard.ToString())}[/]\n");
                            var selection = AnsiConsole.Ask<string>("Seleccione una carta en la pirámide que sume 13 con la carta del stock (formato: fila,col):");
                            var card = selection.Split(',');

                            if (card.Length == 2 &&
                                int.TryParse(card[0], out int row) &&
                                int.TryParse(card[1], out int col) &&
                                IsWithinBounds(row, col))
                            {
                                if (pyramid[row][col] != null && pyramid[row][col].IsFaceUp &&
                                    stockCard.Value + pyramid[row][col].Value == 13)
                                {
                                    UpdateBoard(row, col); // Llamada a la versión de dos parámetros
                                    waste.Remove(stockCard);
                                    stockCard = null;
                                    PlayActionSound();
                                }
                                else
                                {
                                    AnsiConsole.Markup("[bold red]Selección inválida, intenta de nuevo.[/]\n");
                                }
                            }
                            else
                            {
                                AnsiConsole.Markup("[bold red]Entrada no válida, intenta de nuevo.[/]\n");
                            }
                        }
                    }
                    else
                    {
                        AnsiConsole.Markup("[bold red]No hay carta en el stock para usar.[/]\n");
                    }
                }
                else if (input.ToLower() == "k")
                {
                    var selection = AnsiConsole.Ask<string>("Seleccione la posición de la carta K en la pirámide (formato: fila,col):");
                    var card = selection.Split(',');

                    if (card.Length == 2 &&
                        int.TryParse(card[0], out int row) &&
                        int.TryParse(card[1], out int col) &&
                        IsWithinBounds(row, col))
                    {
                        if (pyramid[row][col] != null && pyramid[row][col].IsFaceUp && pyramid[row][col].Value == 13)
                        {
                            UpdateBoard(row, col); // Eliminar la carta K
                            PlayActionSound();
                        }
                        else
                        {
                            AnsiConsole.Markup("[bold red]Selección inválida, intenta de nuevo.[/]\n");
                        }
                    }
                    else
                    {
                        AnsiConsole.Markup("[bold red]Entrada no válida, intenta de nuevo.[/]\n");
                    }
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
                                UpdateBoard(row1, col1, row2, col2); // Llamada a la versión de cuatro parámetros
                                PlayActionSound();
                            }
                            else
                            {
                                AnsiConsole.Markup("[bold red]Selección inválida, intenta de nuevo.[/]\n");
                            }
                        }
                        else
                        {
                            AnsiConsole.Markup("[bold red]Entrada no válida, intenta de nuevo.[/]\n");
                        }
                    }
                    else
                    {
                        AnsiConsole.Markup("[bold red]Entrada no válida, intenta de nuevo.[/]\n");
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
                drawnCard.IsFaceUp = true; // Asegurar que la carta del stock esté boca arriba
                stock.RemoveAt(0);
                waste.Add(drawnCard);
                AnsiConsole.Markup($"[bold yellow]Sacaste una carta del stock: {EscapeMarkup(drawnCard.ToString())}[/]\n");
            }
            else
            {
                AnsiConsole.Markup("[bold red]No hay más cartas en el stock.[/]\n");
            }
        }

        private static bool IsValidSelection(int row1, int col1, int row2, int col2)
        {
            if (!IsWithinBounds(row1, col1) || !IsWithinBounds(row2, col2))
            {
                return false;
            }

            Card card1 = pyramid[row1][col1];
            Card card2 = pyramid[row2][col2];

            if (card1 == null || card2 == null || !card1.IsFaceUp || !card2.IsFaceUp || card1 == card2)
            {
                return false;
            }

            return (card1.Value + card2.Value == 13);
        }

        private static bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < 7 && col >= 0 && col <= row;
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

        private static void UpdateBoard(int row, int col)
        {
            pyramid[row][col] = null;
            score += 10;

            for (int r = 0; r < 7; r++)
            {
                for (int c = 0; c < r + 1; c++)
                {
                    if (pyramid[r][c] != null && !pyramid[r][c].IsFaceUp)
                    {
                        if ((r == 6) || (r < 6 && pyramid[r + 1][c] == null && pyramid[r + 1][c + 1] == null))
                        {
                            pyramid[r][c].IsFaceUp = true;
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
            AnsiConsole.Clear();
            AnsiConsole.Markup("[bold red]¡Juego terminado![/]\n");
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

        private static string EscapeMarkup(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "  ";
            }
            return text.Replace("[", "[").Replace("]", "]");
        }

        private static void DisplayGameBoard(Card stockCard = null)
        {
            Console.Clear();

            Console.WriteLine(
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
                Console.Write("│");
                string rowStr = new string(' ', (7 - row) * 3); // Ajustar el espaciado inicial
                for (int col = 0; col < row + 1; col++)
                {
                    rowStr += "[" + (pyramid[row][col] != null ? EscapeMarkup(pyramid[row][col].ToString()) : "  ") + "] ";
                }
                Console.WriteLine($"{rowStr.PadRight(70)}│"); // Ajustar el ancho del cuadro
            }

            Console.WriteLine(
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
│     Instrucciones:                              Valor de las Cartas:     │
│                                                                          │
│   Se crea un mazo aleatorio, o una pirámide    As: 1 punto               │
│   de cartas, intenta juntar 2 cartas que sus   2 a 10: su valor numérico │
│   valores sumen un total de 13 hasta que se    Jota (J): 11 puntos       │
│   termine la pirámide y podrás ganar,          Reina (Q): 12 puntos      │
│   el                                           Rey (K): 13 puntos        │
│                                                                          │
└──────────────────────────────────────────────────────────────────────────┘
""");

            if (stockCard != null)
            {
                Console.WriteLine($"\nCarta del stock: [{EscapeMarkup(stockCard.ToString())}]\n");
            }
            else
            {
                Console.WriteLine($"\nCarta del stock: [  ]\n");
            }

            Console.WriteLine($"\nStock: {stock.Count} cartas restantes\n");
            Console.WriteLine($"\nWaste: {waste.Count} cartas\n");
            Console.WriteLine($"\nPuntuación: {score}\n");
        }
    }
}
