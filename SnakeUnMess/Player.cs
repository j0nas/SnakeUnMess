namespace SnakeUnMess
{
    using System;
    using System.Linq;

    public class Player
    {
        public Snake Snake { get; set; }

        private bool extendSnake = false;
        private Direction lastDirection = Direction.Right;

        public Player(Coordinate initialSnakePosition)
        {
            this.Score = 0;
            this.Snake = new Snake(initialSnakePosition);
        }

        public int Score { get; private set; }

        public Coordinate SnakeHeadCoordinate { get; private set; }

        public void AddScore(int scoreIncrease)
        {
            this.Score += scoreIncrease;
        }

        public void AteFood(int scoreValue)
        {
            this.AddScore(scoreValue);
            extendSnake = true;
        }

        public SnakePart[] GetSnakeParts()
        {
            return this.Snake.Parts.ToArray();
        }

        public void MoveSnake(Direction direction)
        {
            var directionIsLegal = this.DirectionLegal(direction, lastDirection);
            direction = directionIsLegal ? direction : lastDirection;

            var x = this.Snake.Parts.Last().Coordinate.X;
            var y = this.Snake.Parts.Last().Coordinate.Y;

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
            this.Snake.HeadCoordinate = new Coordinate(x, y);
            this.Snake.Parts.Last().IsHead = false;

            // The new added part is the new head
            this.Snake.Parts.Add(new SnakePart(this.SnakeHeadCoordinate, true));

            if (!extendSnake)
            {
                this.Snake.Parts.Remove(this.Snake.Parts.First());
            }
            else
            {
                extendSnake = false;
            }

            if (directionIsLegal)
            {
                lastDirection = direction;
            }
        }

        public bool SnakeSelfCollided()
        {
            return this.Snake.HasSelfCollided();
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
