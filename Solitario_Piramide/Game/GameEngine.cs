using System;
using PyramidSolitaire.UI;
using PyramidSolitaire.Sounds;
using Solitario_Piramide.Game;
using Solitario_Piramide.UI;

namespace PyramidSolitaire.Game
{
    public class GameEngine
    {
        private readonly Deck deck;
        private readonly Pyramid pyramid;
        private readonly Player player;
        private readonly IRenderer renderer;
        private readonly IMenu menu;

        public GameEngine()
        {
            deck = new Deck();
            pyramid = new Pyramid(deck);
            player = new Player();
            renderer = new Renderer();
            menu = new Menu();
        }

        public void Run()
        {
            SoundPlayer.PlayBackgroundMusic();
            menu.ShowWelcomeMenu();

            bool playAgain = true;
            while (playAgain)
            {
                StartGame();
                Console.WriteLine("Do you want to play again? (y/n)");
                playAgain = Console.ReadLine()?.ToLower() == "y";
            }
        }

        private void StartGame()
        {
            bool gameWon = false;
            while (!gameWon && !IsGameOver())
            {
                renderer.RenderPyramid(pyramid);
                renderer.RenderScore(player.Score);

                var move = GetPlayerMove();
                if (move != null)
                {
                    ExecuteMove(move);
                    SoundPlayer.PlayCardFlipSound();
                }

                gameWon = CheckWinCondition();
            }

            if (gameWon)
            {
                Console.WriteLine("Congratulations! You've won the game!");
                player.AddPoints(100); // Example score for winning
            }
            else
            {
                Console.WriteLine("Game over. Better luck next time!");
            }
        }

        private (Card, Card)? GetPlayerMove()
        {
            Console.WriteLine("Enter the positions of the cards to remove (e.g., 'A1 B2'):");
            var input = Console.ReadLine();

            // Lógica para analizar la entrada y obtener las cartas seleccionadas
            var positions = input?.Split(' ');
            if (positions?.Length == 2)
            {
                var card1 = GetCardAtPosition(positions[0]);
                var card2 = GetCardAtPosition(positions[1]);

                if (card1 != null && card2 != null && card1.Value + card2.Value == 13)
                {
                    return (card1, card2);
                }
            }

            return null;
        }

        private Card GetCardAtPosition(string position)
        {
            // Lógica para traducir una cadena de posición (por ejemplo, 'A1') a un objeto Card
            // Esto dependerá de cómo representes las posiciones de las cartas en la UI
            return null;
        }

        private void ExecuteMove((Card, Card) move)
        {
            var (card1, card2) = move;
            pyramid.RemoveCard(card1);
            pyramid.RemoveCard(card2);
            player.AddPoints(10); // Puntos de ejemplo por un movimiento válido
        }

        private bool IsGameOver()
        {
            // Lógica para determinar si no hay más movimientos válidos posibles
            return false;
        }

        private bool CheckWinCondition()
        {
            // Lógica para verificar si se han eliminado todas las cartas
            return pyramid.AllCardsRemoved();
        }
    }
}
