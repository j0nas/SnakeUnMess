namespace SnakeUnMess
{
    using System;
    using System.Drawing;

    public interface IGameWindow
    {
        int Height { get; }

        int Width { get; }

        Rectangle GetRectangle();

        void Clear();

        void DrawObject(Point o, char representationChar, ConsoleColor representationColor);
    }
}
