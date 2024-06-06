using PyramidSolitaire.Game;

namespace PyramidSolitaire.UI
{
    public interface IRenderer
    {
        void RenderPyramid(Pyramid pyramid);
        void RenderScore(int score);
    }
}
