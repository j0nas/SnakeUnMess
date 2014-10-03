namespace SnakeUnMess
{
    using System;
    using System.Linq;

    public class Player
    {
        public Player(Coordinate initialSnakePosition)
        {
            this.Score = 0;
            this.Snake = new Snake(initialSnakePosition);
        }

        public Snake Snake { get; private set; }

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
