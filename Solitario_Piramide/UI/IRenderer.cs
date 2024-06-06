using Solitario_Piramide.Game;

namespace Solitario_Piramide.UI
{
    public interface IRenderer
    {
        void RenderPyramid(Pyramid pyramid);
        void RenderScore(int score);
    }
}
