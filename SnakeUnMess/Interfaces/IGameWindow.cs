namespace SnakeUnMess.Interfaces
{
    using System;
    using System.Drawing;

    public interface IGameWindow
    {
        int Height { get; }

        int Width { get; }

        Rectangle Rectangle { get; }

        void Clear();

        void DrawObject(Point o, char representationChar, ConsoleColor representationColor);
    }
}
