namespace SnakeUnMess
{
    using System;
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
            var snakeDirection = Snake.MovementDirection.Right;

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
                    var keyPressed = Console.ReadKey().KeyChar;
                    Console.Write(keyPressed);
                    switch (keyPressed)
                    {
                        case 'w':
                            snakeDirection = Snake.MovementDirection.Up;
                            break;
                        case 'a':
                            snakeDirection = Snake.MovementDirection.Left;
                            break;
                        case 's':
                            snakeDirection = Snake.MovementDirection.Down;
                            break;
                        case 'd':
                            snakeDirection = Snake.MovementDirection.Right;
                            break;
                    }
                }

                // Change world
                player.MoveSnake(snakeDirection);

                // If player touches the edges of the console window..
                gameOver = PlayerCollided(player.PlayerSnake.Position);                

                // If self was eaten, ..
                if (!gameOver)
                {
                    for (var i = 0; i < player.PlayerSnake.BodyPartList.Count - 1; i++)
                    {
                        if (player.PlayerSnake.Position.X == player.PlayerSnake.BodyPartList[i].PartCoordinate.X
                            && player.PlayerSnake.Position.Y == player.PlayerSnake.BodyPartList[i].PartCoordinate.Y)
                        {
                            gameOver = true;
                            break;
                        }
                    }

                    // If fooditem was eaten, ..
                    if (player.PlayerSnake.Position.X == foodItem.ItemCoordinate.X && player.PlayerSnake.Position.Y == foodItem.ItemCoordinate.Y)
                    {
                        player.Score += foodItem.ScoreValue;
                        foodItem = new FoodItem(FoodItemValue, new Coordinate(randomNumber.Next(Console.WindowWidth), randomNumber.Next(Console.WindowHeight)));
                        player.PlayerSnake.BodyPartList.Add(new SnakePart(player.PlayerSnake.BodyPartList.Last().PartCoordinate, false));
                    }
                }

                // Render
                Console.Clear();
                GameWindow.DrawSnake(player.PlayerSnake);
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
