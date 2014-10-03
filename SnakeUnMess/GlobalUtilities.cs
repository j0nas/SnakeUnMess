namespace SnakeUnMess
{
    using System;

    public class GlobalUtilities
    {
        public static bool MatchingCoordinates(Coordinate a, Coordinate b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static Coordinate RandomCoordinate(int maxWidth, int maxHeight)
        {
            var random = new Random();
            return new Coordinate(random.Next(maxWidth), random.Next(maxHeight));
        }
    }
}