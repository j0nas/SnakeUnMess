namespace SnakeUnMess.Elements.FoodItem
{
    using System.Drawing;

    using SnakeUnMess.Elements;

    public class FoodItem : GameElementObject
    {
        public FoodItem(Point position, int scoreValue)
        {
            this.ScoreValue = scoreValue;
            this.Position = position;
        }

        public int ScoreValue { get; set; }
    }
}
