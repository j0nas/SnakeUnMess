namespace SnakeUnMess
{
    using System;

    public interface IGameWindow
    {
        int Height { get; }

        int Width { get; }

        void Clear();

        void DrawObject(Coordinate o, char representationChar, ConsoleColor representationColor);
    }
}
