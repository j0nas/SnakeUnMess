namespace SnakeUnMess
{
    using System;
    using System.Collections.Specialized;
    using System.Linq;

    public class GameHandler
    {
        // TODO externalize into config file
        private const int FramesPerSecond = 12;

        private const int FoodItemValue = 10;

        public static void Main(string[] args)
        {
            var player = new Player(new Coordinate(10, 10));
            var gameOver = false;
            var snakeDirection = Direction.Right;

            var randomNumber = new Random();
            var foodItem = new FoodItem(
                FoodItemValue,
                new Coordinate(randomNumber.Next(Console.WindowWidth), randomNumber.Next(Console.WindowHeight)));

            while (!gameOver)
            {
                // Handle user input
                if (Console.KeyAvailable)
                {
                    // TODO separate into inputHandler class
                    var keyPressed = Console.ReadKey().Key;
                    Console.Write(keyPressed);

                    switch (keyPressed)
                    {
                        case ConsoleKey.W:
                            snakeDirection = Direction.Up;
                            break;
                        case ConsoleKey.A:
                            snakeDirection = Direction.Left;
                            break;
                        case ConsoleKey.S:
                            snakeDirection = Direction.Down;
                            break;
                        case ConsoleKey.D:
                            snakeDirection = Direction.Right;
                            break;
                    }
                }

                // Change world
                player.MoveSnake(snakeDirection);

                // If player touches the edges of the console window..
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
                        foodItem = new FoodItem(FoodItemValue, new Coordinate(randomNumber.Next(Console.WindowWidth), randomNumber.Next(Console.WindowHeight)));
                        player.Snake.BodyPartList.Add(new SnakePart(player.Snake.BodyPartList.Last().PartCoordinate, false));
                    }
                }

                // Render
                Console.Clear();
                GameWindow.DrawSnake(player.Snake);
                GameWindow.DrawFoodItem(foodItem);

                System.Threading.Thread.Sleep(1000 / FramesPerSecond);
            }
        }

        private static bool PlayerCollided(Coordinate newCoord)
        {
            return newCoord.X >= Console.WindowWidth - 1 || newCoord.Y >= Console.WindowHeight || newCoord.X <= 0 || newCoord.Y <= 0;
        }
    }
}
