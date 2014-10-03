namespace SnakeUnMess
{
    public class GlobalUtilities
    {


        public static bool MatchingCoordinates(Coordinate a, Coordinate b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
    }
}