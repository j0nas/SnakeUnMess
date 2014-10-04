namespace SnakeUnMess.Elements.Snake
{
    using System.Drawing;

    using SnakeUnMess.Elements;

    public class SnakePart : GameElementObject
    {
        public SnakePart(Point point, bool isHead)
        {
            this.Point = point;
            this.IsHead = isHead;
        }

        public bool IsHead { get; set; }
    }
}