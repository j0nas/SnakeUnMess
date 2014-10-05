namespace SnakeUnMess.Interfaces
{
    using System;
    using System.Drawing;

    using SnakeUnMess.Elements;

    public interface IGameWindow
    {
        int Height { get; }

        int Width { get; }

        Rectangle Rectangle { get; }

        void Clear();

        void DrawObject(Point o, GameObjectType type);

        void DrawGameOver(int score);

        void DrawStartScreen();
    }
}
