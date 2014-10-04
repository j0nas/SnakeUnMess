namespace SnakeUnMess.Elements.Snake
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class Snake
    {
        private const int DefaultSnakeSize = 4;

        private Direction lastDirection = Direction.Right;

        private bool mustExtend;

        public Snake(Point initialHeadPoint, int snakeSize = DefaultSnakeSize)
        {
            this.HeadPoint = initialHeadPoint;
            this.Parts = new List<SnakePart>(snakeSize);


            for (var i = 0; i < snakeSize; i++)
            {
                this.Parts.Add(
                    new SnakePart(new Point(initialHeadPoint.X + i, initialHeadPoint.Y), GameObjectType.Body));
            }
        }

        public Point HeadPoint { get; private set; }

        public List<SnakePart> Parts { get; set; }


        public Point GetNextMovePoint(Direction desiredDirection)
        {
            if (!this.DirectionLegal(desiredDirection))
            {
                desiredDirection = this.lastDirection;
            }
            this.lastDirection = desiredDirection;

            var x = this.Parts.Last().Position.X;
            var y = this.Parts.Last().Position.Y;

            switch (desiredDirection)
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

            return new Point(x, y);
        }

        public void Move(Point point)
        {
            // Update position property (== new position of head)
            this.HeadPoint = point;
            this.Parts.Last().Type = GameObjectType.Body;

            // The new added part is the new head
            this.Parts.Add(new SnakePart(this.HeadPoint, GameObjectType.Head));
            if (!this.mustExtend)
            {
                this.Parts.Remove(this.Parts.First());
            }

            this.mustExtend = false;
        }

        public void Extend()
        {
            this.mustExtend = true;
        }

        private bool DirectionLegal(Direction goalDirection)
        {
            switch (goalDirection)
            {
                case Direction.Up:
                    return this.lastDirection != Direction.Down;
                case Direction.Down:
                    return this.lastDirection != Direction.Up;
                case Direction.Left:
                    return this.lastDirection != Direction.Right;
                case Direction.Right:
                    return this.lastDirection != Direction.Left;
                default:
                    throw new Exception("Illegal direction!");
            }
        }
    }
}