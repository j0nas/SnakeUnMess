namespace SnakeUnmess
{
    using System;
    using System.Drawing;

    public class GlobalUtilities
    {
        public static Point RandomPoint(int maxWidth, int maxHeight)
        {
            var random = new Random();
            return new Point(random.Next(maxWidth), random.Next(maxHeight));
        }
    }
}