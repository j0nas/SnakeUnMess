namespace SnakeUnMess
{
    using System.Drawing;

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