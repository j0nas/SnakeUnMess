namespace SnakeUnMess
{
    public class SnakePart
    {
        public SnakePart(Coordinate partCoordinate, bool isHead)
        {
            this.PartCoordinate = partCoordinate;
            this.IsHead = isHead;
        }

        public Coordinate PartCoordinate { get; set; }

        public bool IsHead { get; set; }
    }
}