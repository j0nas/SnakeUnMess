namespace SnakeUnMess
{
    public class Player
    {
        public Coordinate Position { get; set; }

        public Player()
        {
            this.Score = 0;
            this.PlayerSnake = new Snake();
        }

        // TODO IS THIS WRONG? 
        public Snake.MovementDirection LastMovementDirection { get; set; }

        public Snake PlayerSnake { get; set; }

        public int Score { get; set; }

        public void MoveSnake(Snake.MovementDirection direction)
        {
            this.Position = this.PlayerSnake.Move(direction, this.LastMovementDirection);
            this.LastMovementDirection = direction;
        }
    }
}
