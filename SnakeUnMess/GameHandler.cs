namespace SnakeUnMess
{
    using System;
    using System.Linq;

    public class GameHandler
    {
        // TODO externalize into config file
        private const int FramesPerSecond = 12;

        private const int FoodItemValue = 10;

        public void Start(IGameWindow gameWindow, IInputDevice inputDevice)
        {
            var player = new Player(new Coordinate(10, 10));
            var gameOver = false;
            var snakeDirection = Direction.Right;

            var random = new Random();
            var foodItem = new FoodItem(
                FoodItemValue,
                // TODO: Extract console stuff to the GameWindow class.
                new Coordinate(random.Next(Console.WindowWidth), random.Next(Console.WindowHeight)));

            while (!gameOver)
            {
                // Handle user input
                if (inputDevice.KeyAvailable)
                {
                    // TODO separate into inputHandler 
                    switch (inputDevice.UserRequest)
                    {
                        case UserRequest.Up:
                            snakeDirection = Direction.Up;
                            break;
                        case UserRequest.Left:
                            snakeDirection = Direction.Left;
                            break;
                        case UserRequest.Down:
                            snakeDirection = Direction.Down;
                            break;
                        case UserRequest.Right:
                            snakeDirection = Direction.Right;
                            break;
                    }
                }

                // Change world
                player.MoveSnake(snakeDirection);

                // If player touches the edges of the console gameWindow..
                gameOver = PlayerCollided(player.Snake.Position);                

                // If self was eaten, ..
                if (!gameOver)
                {
                    for (var i = 0; i < player.Snake.BodyPartList.Count - 1; i++)
                    {
                        var bodyPart = player.Snake.BodyPartList[i];

                        if (player.Snake.Position.X == bodyPart.PartCoordinate.X
                            && player.Snake.Position.Y == bodyPart.PartCoordinate.Y)
                        {
                            gameOver = true;
                            break;
                        }
                    }

                    // If fooditem was eaten, ..
                    if (player.Snake.Position.X == foodItem.ItemCoordinate.X && player.Snake.Position.Y == foodItem.ItemCoordinate.Y)
                    {
                        player.Score += foodItem.ScoreValue;
                        foodItem = new FoodItem(FoodItemValue, new Coordinate(random.Next(Console.WindowWidth), random.Next(Console.WindowHeight)));
                        player.Snake.BodyPartList.Add(new SnakePart(player.Snake.BodyPartList.Last().PartCoordinate, false));
                    }
                }

                // Render
                gameWindow.Clear();
                gameWindow.DrawSnake(player.Snake);
                gameWindow.DrawFoodItem(foodItem);

                System.Threading.Thread.Sleep(1000 / FramesPerSecond);
            }
        }

        private static bool PlayerCollided(Coordinate newCoord)
        {
            return newCoord.X >= Console.WindowWidth - 1 || newCoord.Y >= Console.WindowHeight || newCoord.X <= 0 || newCoord.Y <= 0;
        }
    }
}
