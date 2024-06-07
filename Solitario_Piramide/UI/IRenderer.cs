using Solitario_Piramide.Interfaces;

namespace Solitario_Piramide.UI
{
    public interface IRenderer
    {
        void RenderPyramid(IPyramid pyramid, int score, (int, int)? selectedCardPosition);
    }
}