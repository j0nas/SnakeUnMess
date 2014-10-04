namespace SnakeUnmess
{
    using System.Drawing;

    public class Player
    {
        public Player(Point initialSnakePosition)
        {
            this.Score = 0;
            this.Snake = new Snake(initialSnakePosition);
        }

        public Snake Snake { get; private set; }

        public int Score { get; private set; }

        public void AteFood(int scoreValue)
        {
            this.Score += scoreValue;
            this.Snake.Extend();
        }
    }
}
