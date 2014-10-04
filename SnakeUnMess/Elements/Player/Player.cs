namespace SnakeUnMess.Elements.Player
{
    using System.Drawing;

    using SnakeUnMess.Elements.Snake;

    public class Player
    {
        public Player(Point initialSnakePosition)
        {
            this.Score = 0;
            this.Snake = new Snake(initialSnakePosition);
        }

        public Snake Snake { get; private set; }

        public int Score { get; private set; }

        public void Scored(int scoreValue)
        {
            this.Score += scoreValue;
            this.Snake.Extend();
        }
    }
}
