namespace SnakeUnMess
{
    public class FoodItem : GameElementObject
    {
        public FoodItem(int scoreValue, Coordinate itemCoordinate)
        {
            this.ScoreValue = scoreValue;
            this.Coordinate = itemCoordinate;
        }

        public int ScoreValue { get; set; }
    }
}
