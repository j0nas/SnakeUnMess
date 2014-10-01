namespace SnakeUnMess
{
    public class Player
    {
        public Player(Coordinate initialSnakePosition)
        {
            this.Score = 0;
            this.Snake = new Snake(initialSnakePosition);
        }

        // TODO IS THIS WRONG? 
        public Direction LastDirection { get; set; }

        public Snake Snake { get; set; }

        public int Score { get; set; }

        public void MoveSnake(Direction direction)
        {
            this.Snake.Move(direction, this.LastDirection);
            this.LastDirection = direction;
        }
    }
}
