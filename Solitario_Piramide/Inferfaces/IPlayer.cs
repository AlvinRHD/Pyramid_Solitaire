namespace Solitario_Piramide.Inferfaces
{
    public interface IPlayer
    {
        void ResetScore(); // Nuevo método para reiniciar la puntuación del jugador
        int Score { get; } // Propiedad para obtener la puntuación del jugador
    }
}