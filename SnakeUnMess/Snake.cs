namespace SnakeUnMess
{
    using System.Collections.Generic;

    public class Snake
    {
        private const int DefaultSnakeSize = 4;

        public Snake(Coordinate initialHeadCoordinate, int snakeSize = DefaultSnakeSize)
        {
            this.HeadCoordinate = initialHeadCoordinate;
            this.Parts = new List<SnakePart>(snakeSize);


            for (var i = 0; i < snakeSize; i++)
            {
                this.Parts.Add(
                    new SnakePart(new Coordinate(initialHeadCoordinate.X + i, initialHeadCoordinate.Y), false));
            }
        }

        public Coordinate HeadCoordinate { get; set; }

        public List<SnakePart> Parts { get; set; }

        public bool HasSelfCollided()
        {
            // Since we're checking for collision with the Head (== Parts[0]), omit the head in the check.
            for (var i = 0; i < this.Parts.Count - 1; i++)
            {
                if (GlobalUtilities.MatchingCoordinates(this.HeadCoordinate, this.Parts[i].Coordinate))
                {
                    return true;
                }
            }

            return false;
        }
    }
}