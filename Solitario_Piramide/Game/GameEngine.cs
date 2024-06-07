using Solitario_Piramide.Interfaces;
using Solitario_Piramide.Sound;
using Solitario_Piramide.UI;
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
            SoundManager soundManager = new SoundManager();
            soundManager.PlayBackgroundMusic();

            bool gameWon = false;
            while (!gameWon && !IsGameOver())
            {
                renderer.RenderPyramid(pyramid, player.Score);

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
            // Ejemplo de lógica de fin de juego (puedes adaptarlo según tus reglas)
            return pyramid.Rows.All(row => row.All(card => card == null));
        }

        private (ICard, ICard)? GetPlayerMove()
        {
            // Aquí puedes pedir al jugador que seleccione dos cartas
            Console.WriteLine("Selecciona la primera carta (fila, columna): ");
            string[] firstInput = Console.ReadLine().Split(',');
            int firstRow = int.Parse(firstInput[0]);
            int firstCol = int.Parse(firstInput[1]);

            Console.WriteLine("Selecciona la segunda carta (fila, columna): ");
            string[] secondInput = Console.ReadLine().Split(',');
            int secondRow = int.Parse(secondInput[0]);
            int secondCol = int.Parse(secondInput[1]);

            var firstCard = pyramid.GetCardAtPosition(firstRow, firstCol);
            var secondCard = pyramid.GetCardAtPosition(secondRow, secondCol);

            return (firstCard, secondCard);
        }

        private void ExecuteMove((ICard, ICard) move)
        {
            // Ejemplo de lógica para ejecutar el movimiento del jugador
            var (firstCard, secondCard) = move;
            if (firstCard != null && secondCard != null && firstCard.Value + secondCard.Value == 13)
            {
                RemoveCard(firstCard);
                RemoveCard(secondCard);
                player.AddPoints(10); // Añadir puntos según tus reglas
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
                    return;
                }
            }
        }

    }
}
