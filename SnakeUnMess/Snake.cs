namespace SnakeUnMess
{
    using System.Collections.Generic;
    using System.Linq;

    public class Snake
    {
        // TODO config file?
        private const int DefaultSnakeSize = 4;

        public Snake(Coordinate initialPosition, int snakeSize = DefaultSnakeSize)
        {
            this.Position = initialPosition;
            this.BodyPartList = new List<SnakePart>(snakeSize);

            for (var i = 0; i < snakeSize; i++)
            {
                this.BodyPartList.Add(new SnakePart(new Coordinate(initialPosition.X + i, initialPosition.Y), false));
            }
        }

        public enum MovementDirection
        {
            Up = -1, Down = 1, Left = -2, Right = 2
        }

        public Coordinate Position { get; set; }
        
        public List<SnakePart> BodyPartList { get; set; }

        public void Move(MovementDirection direction, MovementDirection previousDirection)
        {
            // TODO use these.
            int x, y;

            switch (direction)
            {
                case MovementDirection.Up:
                case MovementDirection.Down:
                    this.Position = new Coordinate(this.BodyPartList.Last().PartCoordinate.X, this.BodyPartList.Last().PartCoordinate.Y + (int)direction);
                    break;
                case MovementDirection.Right: // TODO fix this stuff with math awesomeness (no more switchcase)
                    this.Position = new Coordinate(this.BodyPartList.Last().PartCoordinate.X + 1, this.BodyPartList.Last().PartCoordinate.Y);
                    break;
                case MovementDirection.Left:
                    this.Position = new Coordinate(this.BodyPartList.Last().PartCoordinate.X - 1, this.BodyPartList.Last().PartCoordinate.Y);
                    break;
            }

            this.BodyPartList.Last().IsHead = false;
            this.BodyPartList.Add(new SnakePart(this.Position, true));
            this.BodyPartList.Remove(this.BodyPartList.First());
        }
    }
}