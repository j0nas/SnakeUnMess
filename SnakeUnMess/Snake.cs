namespace SnakeUnMess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Snake
    {
        // TODO config file?
        private const int DefaultSnakeSize = 4;

        public Snake(Coordinate initialHeadCoordinate, int snakeSize = DefaultSnakeSize)
        {
            this.HeadCoordinate = initialHeadCoordinate;
            this.Parts = new List<SnakePart>(snakeSize);

            for (var i = 0; i < snakeSize; i++)
            {
                this.Parts.Add(new SnakePart(new Coordinate(initialHeadCoordinate.X + i, initialHeadCoordinate.Y), false));
            }
        }

        public Coordinate HeadCoordinate { get; set; }

        public List<SnakePart> Parts { get; set; }

        public void Move(Direction direction, Direction previousDirection)
        {
            var x = this.Parts.Last().PartCoordinate.X;
            var y = this.Parts.Last().PartCoordinate.Y;

            switch (direction)
            {
                case Direction.Up:
                    y--;
                    break;
                case Direction.Down:
                    y++;
                    break;
                case Direction.Right:
                    x++;
                    break;
                case Direction.Left:
                    x--;
                    break;
            }

            // Update position property (== new position of head)
            this.HeadCoordinate = new Coordinate(x, y);

            this.Parts.Last().IsHead = false;
            // The new added part is the new head
            this.Parts.Add(new SnakePart(this.HeadCoordinate, true));
            this.Parts.Remove(this.Parts.First());
        }

        public bool HasPerformedCannibalism()
        {
            // Since we're checking for collision with the Head (== Parts[0]), omit the head in the check.
            for (var i = 0; i < this.Parts.Count - 1; i++)
            {
                if (GlobalUtilities.MatchingCoordinates(this.HeadCoordinate, this.Parts[i].PartCoordinate))
                {
                    return true;
                }
            }

            return false;
        }
    }
}