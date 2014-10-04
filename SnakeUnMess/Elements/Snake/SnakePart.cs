namespace SnakeUnMess.Elements.Snake
{
    using System.Drawing;

    using SnakeUnMess.Elements;

    public class SnakePart : GameElementObject
    {
        public SnakePart(Point position, bool isHead)
        {
            this.Position = position;
            this.IsHead = isHead;
        }

        public bool IsHead { get; set; }
    }
}