namespace SnakeUnMess
{
    public interface IGameWindow
    {
        int WindowHeight { get; }

        int WindowWidth { get; }

        void Clear();

        void DrawSnake(Snake snake);

        void DrawFoodItem(FoodItem foodItem);
    }
}
