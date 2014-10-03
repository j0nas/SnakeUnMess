namespace SnakeUnMess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Snake
    {
        private const int DefaultSnakeSize = 4;

        private Direction lastDirection = Direction.Right;

        private bool mustExtend = false;

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

        public Coordinate HeadCoordinate { get; private set; }

        public List<SnakePart> Parts { get; set; }

        public void Move(Direction direction)
        {
            var directionIsLegal = this.DirectionLegal(direction, lastDirection);

            if (!directionIsLegal)
            {
                direction = lastDirection;
            }
            else
            {
                lastDirection = direction;
            }

            var x = this.Parts.Last().Coordinate.X;
            var y = this.Parts.Last().Coordinate.Y;

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
            if (this.mustExtend)
            {
                mustExtend = false;
            }
            else
            {
                this.Parts.Remove(this.Parts.First());
            }
        }

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

        public void Extend()
        {
            this.mustExtend = true;
        }

        private bool DirectionLegal(Direction goalDirection, Direction previousDirection)
        {
            switch (goalDirection)
            {
                case Direction.Up:
                    return previousDirection != Direction.Down;
                case Direction.Down:
                    return previousDirection != Direction.Up;
                case Direction.Left:
                    return previousDirection != Direction.Right;
                case Direction.Right:
                    return previousDirection != Direction.Left;
                default:
                    throw new Exception("Illegal direction!");
            }
        }
    }
}