using System;
using Solitario_Piramide.Game;
using Solitario_Piramide.Inferfaces;
using Solitario_Piramide.Sound;
using Solitario_Piramide.UI;


namespace Solitario_Piramide
{
    class Program
    {
        static void Main(string[] args)
        {
            IPyramid pyramid = new Pyramid();
            pyramid.Rows.Add(new List<ICard> { new Card(1, "Hearts") });
            pyramid.Rows.Add(new List<ICard> { new Card(2, "Diamonds"), new Card(3, "Clubs") });
            pyramid.Rows.Add(new List<ICard> { new Card(4, "Spades"), new Card(5, "Hearts"), new Card(6, "Diamonds") });

            IRenderer renderer = new Renderer();
            renderer.RenderPyramid(pyramid);

            //ISoundManager soundManager = new SoundManager();
            //soundManager.PlayBackgroundMusic();
        }
    }
}