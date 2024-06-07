using Solitario_Piramide.Interfaces;

namespace Solitario_Piramide.Interfaces
{
    public interface IRenderer
    {
        void RenderPyramid(IPyramid pyramid, int score, (int, int)? selectedCardPosition);
    }
}