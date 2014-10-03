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
                new Coordinate(random.Next(gameWindow.WindowWidth), random.Next(gameWindow.WindowHeight)));

            while (!gameOver)
            {
                // Handle user input using IInputDevice from ConsoleGameClient
                if (inputDevice.KeyAvailable)
                {
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

                // If player's snake touches the edges of the console gameWindow or eats itself ..
                // (Used && and not || since && doesn't evaluate second part of statement if first is false)
                gameOver = !(!PlayerCollided(player.Snake.HeadCoordinate) && !player.Snake.HasPerformedCannibalism());                

                if (!gameOver)
                {
                    // If fooditem was eaten, ..
                    if (GlobalUtilities.MatchingCoordinates(player.Snake.HeadCoordinate, foodItem.ItemCoordinate))
                    {
                        player.Score += foodItem.ScoreValue; // TODO extract console stuff to ConsoleGameWindow
                        foodItem = new FoodItem(FoodItemValue, new Coordinate(random.Next(Console.WindowWidth), random.Next(Console.WindowHeight)));
                        player.Snake.Parts.Add(new SnakePart(player.Snake.Parts.Last().PartCoordinate, false));
                    }
                }

                // Render
                gameWindow.Clear();
                gameWindow.DrawSnake(player.Snake);
                gameWindow.DrawFoodItem(foodItem);

                System.Threading.Thread.Sleep(1000 / FramesPerSecond); // TODO calculate time frame
            }
        }


        private static bool PlayerCollided(Coordinate newCoordinate)
        {
            return newCoordinate.X >= Console.WindowWidth - 1 || newCoordinate.Y >= Console.WindowHeight || newCoordinate.X <= 0 || newCoordinate.Y <= 0;
        }
    }
}
