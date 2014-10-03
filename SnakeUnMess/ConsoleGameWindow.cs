﻿namespace SnakeUnMess
{
    using System;

    public class ConsoleGameWindow : IGameWindow
    {
        private const char SnakeBodyRepresentationChar = '@';

        private const char SnakeHeadRepresentationChar = '>';

        private const char FoodItemRepresentationChar = '$';

        public ConsoleGameWindow()
        {
            this.WindowHeight = Console.WindowHeight;
            this.WindowWidth = Console.WindowHeight;
        }        

        public int WindowHeight { get; private set; }

        public int WindowWidth { get; private set; }

        public void Clear()
        {
            Console.Clear();
        }

        public void DrawSnake(Snake snake)
        {
            var currentConsoleColor = Console.ForegroundColor;
            foreach (var t in snake.Parts)
            {
                Console.SetCursorPosition(t.PartCoordinate.X, t.PartCoordinate.Y);
                Console.ForegroundColor = t.IsHead ? ConsoleColor.DarkCyan : ConsoleColor.DarkBlue; // TODO EXTRACT COLORS TO CONFIG
                Console.Write(t.IsHead ? SnakeHeadRepresentationChar : SnakeBodyRepresentationChar);
            }
            Console.ForegroundColor = currentConsoleColor;
        }

        public void DrawFoodItem(FoodItem foodItem)
        {
            Console.SetCursorPosition(foodItem.ItemCoordinate.X, foodItem.ItemCoordinate.Y);
            Console.Write(FoodItemRepresentationChar);
        }
    }
}
