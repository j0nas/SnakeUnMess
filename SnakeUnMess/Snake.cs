namespace SnakeUnMess
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class Snake
    {
        // TODO config file?
        private const int DefaultSnakeSize = 4;

        public Snake(Coordinate startingPosition, int snakeSize = DefaultSnakeSize)
        {
            this.BodyPartList = new List<SnakePart>(snakeSize);

            for (var i = 0; i < snakeSize; i++)
            {
                this.BodyPartList.Add(new SnakePart(new Coordinate(startingPosition.X + i, startingPosition.Y), false));
            }
        }

        public enum MovementDirection
        {
            Up = -1, Down = 1, Left = -2, Right = 2
        }

        private enum DefaultStartingPositionX
        {
            Left = 0, Center = 40, Right = 100
        }

        private enum DefaultStartingPositionY
        {
            Top = 0, Center = 10, Bottom = 100
        }

        public List<SnakePart> BodyPartList { get; set; }

        public Coordinate Move(MovementDirection direction, MovementDirection previousDirection)
        {
            Coordinate newCoords = null;

            switch (direction)
            {
                case MovementDirection.Up:
                case MovementDirection.Down:
                    newCoords = new Coordinate(this.BodyPartList.Last().PartCoordinate.X, this.BodyPartList.Last().PartCoordinate.Y + (int)direction);
                    break;
                case MovementDirection.Right: // TODO fix this stuff with math awesomeness (no more switchcase)
                    newCoords = new Coordinate(this.BodyPartList.Last().PartCoordinate.X + 1, this.BodyPartList.Last().PartCoordinate.Y);
                    break;
                case MovementDirection.Left:
                    newCoords = new Coordinate(this.BodyPartList.Last().PartCoordinate.X - 1, this.BodyPartList.Last().PartCoordinate.Y);
                    break;
            }

            this.BodyPartList.Last().IsHead = false;
            this.BodyPartList.Add(new SnakePart(newCoords, true));
            this.BodyPartList.Remove(this.BodyPartList.First());


            return this.BodyPartList.Last().PartCoordinate;
        }
    }
}