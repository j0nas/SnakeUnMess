namespace SnakeUnMess
{
    using System;

    public class GameWindow
    {
        private const char SnakeBodyRepresentationChar = '@';

        private const char SnakeHeadRepresentationChar = '>';

        private const char FoodItemRepresentationChar = '$';

        public static void DrawSnake(Snake snake)
        {
            var currentConsoleColor = Console.ForegroundColor;
            foreach (var t in snake.BodyPartList)
            {
                Console.SetCursorPosition(t.PartCoordinate.X, t.PartCoordinate.Y);
                Console.ForegroundColor = t.IsHead ? ConsoleColor.DarkCyan : ConsoleColor.DarkBlue;
                Console.Write(t.IsHead ? SnakeHeadRepresentationChar : SnakeBodyRepresentationChar);
            }
            Console.ForegroundColor = currentConsoleColor;
        }

        public static void DrawFoodItem(FoodItem foodItem)
        {
            Console.SetCursorPosition(foodItem.ItemCoordinate.X, foodItem.ItemCoordinate.Y);
            Console.Write(FoodItemRepresentationChar);
        }
    }
}
