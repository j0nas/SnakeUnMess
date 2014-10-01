namespace SnakeUnMess
{
    public class Player
    {
        public Player(Coordinate initialSnakePosition)
        {
            this.Score = 0;
            this.PlayerSnake = new Snake(initialSnakePosition);
        }

        // TODO IS THIS WRONG? 
        public Snake.MovementDirection LastMovementDirection { get; set; }

        public Snake PlayerSnake { get; set; }

        public int Score { get; set; }

        public void MoveSnake(Snake.MovementDirection direction)
        {
            this.PlayerSnake.Move(direction, this.LastMovementDirection);
            this.LastMovementDirection = direction;
        }
    }
}
