namespace SnakeUnMess
{
    using System;

    public class Player
    {
        public Player(Coordinate initialSnakePosition)
        {
            this.Score = 0;
            this.Snake = new Snake(initialSnakePosition);
        }

        public Direction LastDirection { get; set; }

        public Snake Snake { get; set; }

        public int Score { get; set; }

        public void MoveSnake(Direction direction)
        {
            var directionIsLegal = this.DirectionLegal(direction, LastDirection);
            this.Snake.Move(directionIsLegal ? direction : LastDirection, this.LastDirection);
            if (directionIsLegal)
            {
                // Prevent reverse movement; up when lastDirection is Down, ect.
                this.LastDirection = direction;                
            }
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
