namespace SnakeUnMess
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Coordinate(Coordinate coordinate)
        {
            this.X = coordinate.X;
            this.Y = coordinate.Y;
        }

        protected Coordinate()
        {
            throw new System.NotImplementedException();
        }

        public int X { get; set; }

        public int Y { get; set; }
    }
}
