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

        public Snake(Coordinate initialPosition, int snakeSize = DefaultSnakeSize)
        {
            this.Position = initialPosition;
            this.Parts = new List<SnakePart>(snakeSize);

            for (var i = 0; i < snakeSize; i++)
            {
                this.Parts.Add(new SnakePart(new Coordinate(initialPosition.X + i, initialPosition.Y), false));
            }
        }

        public Coordinate Position { get; set; }

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

            this.Position = new Coordinate(x, y);

            this.Parts.Last().IsHead = false;
            this.Parts.Add(new SnakePart(this.Position, true));
            this.Parts.Remove(this.Parts.First());
        }
    }
}