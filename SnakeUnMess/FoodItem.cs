namespace SnakeUnMess
{
    public class FoodItem
    {
        public FoodItem(int scoreValue, Coordinate itemCoordinate)
        {
            this.ScoreValue = scoreValue;
            this.ItemCoordinate = itemCoordinate;
        }

        public int ScoreValue { get; set; }

        public Coordinate ItemCoordinate { get; set; }
    }
}
