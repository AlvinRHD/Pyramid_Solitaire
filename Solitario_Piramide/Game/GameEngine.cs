using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Solitario_Piramide.UI;

namespace Solitario_Piramide.Game
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

            // Logic to parse input and get the selected cards
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
            // Logic to translate a position string (e.g., 'A1') to a Card object
            // This will depend on how you represent the card positions in the UI
            return null;
        }

        private void ExecuteMove((Card, Card) move)
        {
            var (card1, card2) = move;
            pyramid.RemoveCard(card1);
            pyramid.RemoveCard(card2);
            player.AddPoints(10); // Example points for a valid move
        }

        private bool IsGameOver()
        {
            // Logic to determine if no more valid moves are possible
            return false;
        }

        private bool CheckWinCondition()
        {
            // Logic to check if all cards have been removed
            return pyramid.AllCardsRemoved();
        }
    }
}
