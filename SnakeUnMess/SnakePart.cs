namespace SnakeUnMess
{
    public class SnakePart : GameElementObject
    {
        public SnakePart(Coordinate coordinate, bool isHead)
        {
            this.Coordinate = coordinate;
            this.IsHead = isHead;
        }

        public bool IsHead { get; set; }
    }
}