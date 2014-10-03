namespace SnakeUnMess
{
    using System;

    public class ConsoleGameWindow : IGameWindow
    {
        public ConsoleGameWindow()
        {
            this.Height = Console.WindowHeight;
            this.Width = Console.WindowWidth;
        }        

        public int Height { get; private set; }

        public int Width { get; private set; }

        public void Clear()
        {
            Console.Clear();
        }

        public void DrawObject(Coordinate o, char representationChar, ConsoleColor representationColor)
        {
            var tempColorChange = Console.ForegroundColor;
            Console.SetCursorPosition(o.X, o.Y);
            Console.ForegroundColor = representationColor;
            Console.Write(representationChar);
            Console.ForegroundColor = tempColorChange;
        }
    }
}
