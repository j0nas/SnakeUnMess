namespace SnakeUnMess
{
    public class GlobalUtilities
    {
        public static bool MatchingCoordinates(Coordinate a, Coordinate b) // TODO move into global utility class?
        {
            return a.X == b.X && a.Y == b.Y;
        }
    }
}