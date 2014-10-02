namespace SnakeUnMess
{
    public interface IGameWindow
    {
        void Clear();

        void DrawSnake(Snake snake);

        void DrawFoodItem(FoodItem foodItem);

        int WindowHeight { get; set; }
        int WindowWidth { get; set; }
    }
}
