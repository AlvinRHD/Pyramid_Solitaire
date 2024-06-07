using Solitario_Piramide.Interfaces;
using Solitario_Piramide.Sound;
using Solitario_Piramide.UI;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace Solitario_Piramide.Game.GameEngine
{
    public class GameEngine
    {
        private Pyramid pyramid;
        private Renderer renderer;
        private Player player;
        private Deck deck;
        private (int, int)? additionalCard;

        public GameEngine()
        {
            pyramid = new Pyramid();
            renderer = new Renderer();
            player = new Player();
            deck = new Deck();
            additionalCard = null;

            InitializePyramid();
        }

        private void InitializePyramid()
        {
            deck.Shuffle();
            pyramid.SetUpPyramid(deck);
        }

        public void StartGame()
        {
            SoundManager soundManager = new SoundManager();
            soundManager.PlayBackgroundMusic();

            bool gameWon = false;

            while (!gameWon && !IsGameOver())
            {
                renderer.RenderPyramid(pyramid, player.Score, additionalCard);

                var move = GetPlayerMove();
                if (move != null)
                {
                    ExecuteMove(move.Value);
                    soundManager.PlayCardFlipSound();
                }

                gameWon = CheckWinCondition();
            }

            if (gameWon)
            {
                AnsiConsole.Markup("[bold green]Congratulations! You've won the game![/]");
                player.AddPoints(100); // Example score for winning
            }
            else
            {
                AnsiConsole.Markup("[bold red]Game over. Better luck next time![/]");
            }
        }

        private bool IsGameOver()
        {
            // Check if all cards have been removed
            bool allCardsRemoved = pyramid.Rows.All(row => row.All(card => card == null));
            if (allCardsRemoved)
            {
                return true;
            }

            // Check if there are possible moves
            for (int i = 0; i < pyramid.Rows.Count; i++)
            {
                for (int j = 0; j < pyramid.Rows[i].Count; j++)
                {
                    var card1 = pyramid.Rows[i][j];
                    if (card1 == null) continue;
                    for (int k = 0; k < pyramid.Rows.Count; k++)
                    {
                        for (int l = 0; l < pyramid.Rows[k].Count; l++)
                        {
                            if (i == k && j == l) continue;
                            var card2 = pyramid.Rows[k][l];
                            if (card2 != null && card1.Value + card2.Value == 13)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            // Check if there are possible moves with additional cards
            if (additionalCard != null)
            {
                for (int i = 0; i < pyramid.Rows.Count; i++)
                {
                    for (int j = 0; j < pyramid.Rows[i].Count; j++)
                    {
                        var pyramidCard = pyramid.Rows[i][j];
                        if (pyramidCard != null && additionalCard.Value + pyramidCard.Value == 13)
                        {
                            return false;
                        }
                    }
                }
            }

            // If no possible moves and no cards left in the deck
            if (deck.CardsRemaining == 0 && additionalCard == null)
            {
                return true;
            }

            return false;
        }

        private (ICard, ICard)? GetPlayerMove()
        {
            try
            {
                var options = new List<string> { "draw" };
                for (int i = 0; i < pyramid.Rows.Count; i++)
                {
                    for (int j = 0; j < pyramid.Rows[i].Count; j++)
                    {
                        if (pyramid.Rows[i][j] != null)
                        {
                            options.Add($"{i},{j}");
                        }
                    }
                }

                var firstInput = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select the first card (row, column) or [green]draw[/] to draw a card from the deck:")
                        .AddChoices(options));

                if (firstInput == "draw")
                {
                    if (deck.CardsRemaining > 0)
                    {
                        additionalCard = deck.DrawCard();
                        return null;
                    }
                    else
                    {
                        AnsiConsole.Markup("[red]No cards left in the deck.[/]");
                        return null;
                    }
                }

                var firstCoords = firstInput.Split(',');
                int firstRow = int.Parse(firstCoords[0]);
                int firstCol = int.Parse(firstCoords[1]);

                var secondInput = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Select the second card (row, column) or [green]draw[/] to draw a card from the deck:")
                        .AddChoices(options));

                if (secondInput == "draw")
                {
                    if (deck.CardsRemaining > 0)
                    {
                        additionalCard = deck.DrawCard();
                        return null;
                    }
                    else
                    {
                        AnsiConsole.Markup("[red]No cards left in the deck.[/]");
                        return null;
                    }
                }

                var secondCoords = secondInput.Split(',');
                int secondRow = int.Parse(secondCoords[0]);
                int secondCol = int.Parse(secondCoords[1]);

                var firstCard = pyramid.GetCardAtPosition(firstRow, firstCol);
                var secondCard = pyramid.GetCardAtPosition(secondRow, secondCol);

                return (firstCard, secondCard);
            }
            catch (Exception ex)
            {
                AnsiConsole.Markup($"[red]{ex.Message}[/]");
                return null;
            }
        }

        private void ExecuteMove((ICard, ICard) move)
        {
            var (firstCard, secondCard) = move;
            if (firstCard != null && secondCard != null && firstCard.Value + secondCard.Value == 13)
            {
                player.AddPoints(10); // Puntaje según tus reglas
            }
        }

        private bool CheckWinCondition()
        {
            // Verificar si todas las cartas han sido eliminadas
            return pyramid.Rows.All(row => row.All(card => card == null));
        }

        private void RemoveCard(ICard card)
        {
            // Eliminar la carta de la pirámide
            foreach (var row in pyramid.Rows)
            {
                if (row.Contains(card))
                {
                    row[row.IndexOf(card)] = null;
                    break;
                }
            }
        }

        public void RestartGame()
        {
            pyramid = new Pyramid();
            deck.ResetDeck();
            player.ResetScore();
            additionalCard = null;
            InitializePyramid();
        }
    } 
}  
