namespace SnakeUnMess
{
    using System;
    using System.Linq;

    public class Player
    {
        public Snake Snake { get; private set; }

        private bool extendSnake = false;
        private Direction lastDirection = Direction.Right;

        public Player(Coordinate initialSnakePosition)
        {
            this.Score = 0;
            this.Snake = new Snake(initialSnakePosition);
        }

        public int Score { get; private set; }

        public void AddScore(int scoreIncrease)
        {
            this.Score += scoreIncrease;
        }

        public void AteFood(int scoreValue)
        {
            this.AddScore(scoreValue);
            this.Snake.Extend();
        }

        public void MoveSnake(Direction direction)
        {
            this.Snake.Move(direction);
        }

        public bool SnakeSelfCollided()
        {
            return this.Snake.HasSelfCollided();
        }
    }
}
