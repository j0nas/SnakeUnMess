namespace SnakeUnMess
{
    using System.Security.Cryptography.X509Certificates;

    public interface IGameWindow
    {
        void Clear();

        void DrawSnake(Snake snake);

        void DrawFoodItem(FoodItem foodItem);
    }
}
