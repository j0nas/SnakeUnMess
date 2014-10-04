namespace SnakeUnMess.Elements.Snake
{
    using System.Drawing;

    using SnakeUnMess.Elements;

    public class SnakePart : GameElementObject
    {
        public SnakePart(Point position, GameObjectType type)
        {
            this.Position = position;
            this.Type = type;
        }

        public GameObjectType Type { get; set; }
    }
}