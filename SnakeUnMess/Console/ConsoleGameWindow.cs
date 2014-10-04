namespace SnakeUnMess.Console
{
    using System;
    using System.Drawing;

    using SnakeUnMess.Interfaces;

    public class ConsoleGameWindow : IGameWindow
    {
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

        public void DrawObject(Point o, char representationChar, ConsoleColor representationColor)
        {
            var tempColorChange = Console.ForegroundColor;
            Console.SetCursorPosition(o.X, o.Y);
            Console.ForegroundColor = representationColor;
            Console.Write(representationChar);
            Console.ForegroundColor = tempColorChange;
        }
    }
}
