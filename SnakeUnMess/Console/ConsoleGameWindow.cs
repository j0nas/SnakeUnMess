namespace SnakeUnMess.Console
{
    using System;
    using System.Drawing;

    using SnakeUnMess.Elements;
    using SnakeUnMess.Interfaces;

    public class ConsoleGameWindow : IGameWindow
    {
        private const char SnakeBodyRepresentationChar = 'O';

        private const char SnakeHeadRepresentationChar = '@';

        private const ConsoleColor SnakeHeadColor = ConsoleColor.Green;

        private const ConsoleColor SnakeBodyColor = ConsoleColor.Green;

        private const char FoodItemRepresentationChar = '$';

        private const ConsoleColor FoodItemColor = ConsoleColor.Red;

        public ConsoleGameWindow()
        {
            this.Height = Console.WindowHeight;
            this.Width = Console.WindowWidth;
            this.Rectangle = new Rectangle(0, 0, this.Width, this.Height);
        }        

        public int Height { get; private set; }

        public int Width { get; private set; }

        public Rectangle Rectangle { get; private set; }

        public void Clear()
        {
            Console.Clear();
        }

        public void DrawObject(Point o, GameObjectType type)
        {
            Console.SetCursorPosition(o.X, o.Y);
            var previousForegroundColor = Console.ForegroundColor;

            switch (type)
            {
                case GameObjectType.Head:
                    Console.ForegroundColor = SnakeHeadColor;
                    Console.Write(SnakeHeadRepresentationChar);
                    break;
                case GameObjectType.Body:
                    Console.ForegroundColor = SnakeBodyColor;
                    Console.Write(SnakeBodyRepresentationChar);
                    break;
                case GameObjectType.Food:
                    Console.ForegroundColor = FoodItemColor;
                    Console.Write(FoodItemRepresentationChar);
                    break;
                default:
                    throw new Exception("Type '" + type + "' is not a supported GameObjectType.");
            }

            Console.ForegroundColor = previousForegroundColor;
        }
    }
}
