using Solitario_Piramide.Sound;
using Solitario_Piramide.UI;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Solitario_Piramide.Game.GameEngine
{
    public class GameEngine
    {
        private Pyramid pyramid;
        private Renderer renderer;
        private Player player;
        private Deck deck;

        public GameEngine()
        {
            pyramid = new Pyramid();
            renderer = new Renderer();
            player = new Player();
            deck = new Deck();

            InitializePyramid();
        }

        private void InitializePyramid()
        {
            deck.Shuffle();
            pyramid.SetUpPyramid(deck);
        }

        public void StartGame()
        {
            bool gameWon = false;
            while (!gameWon && !IsGameOver())
            {
                renderer.Render(pyramid, player.Score);

                var move = GetPlayerMove();
                if (move != null)
                {
                    ExecuteMove(move);
                    SoundManager.PlayCardFlipSound();
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

        private bool IsGameOver()
        {
            // Logic to determine if the game is over
            return false;
        }

        private (Card, Card)? GetPlayerMove()
        {
            // Logic to get the player's move
            return null;
        }

        private void ExecuteMove((Card, Card) move)
        {
            // Logic to execute the player's move
        }

        private bool CheckWinCondition()
        {
            // Logic to check if the player has won
            return false;
        }
    }
}
