namespace SnakeUnMess.Elements.FoodItem
{
    using System.Drawing;

    using SnakeUnMess.Elements;

    public class FoodItem : GameElementObject
    {
        public FoodItem(Point point, int scoreValue)
        {
            this.ScoreValue = scoreValue;
            this.Point = point;
        }

        public int ScoreValue { get; set; }
    }
}
