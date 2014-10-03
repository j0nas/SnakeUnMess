namespace SnakeUnMess
{
    using System;
    using System.Linq;
    using System.Threading;

    public class GameHandler
    {
        // TODO externalize into config file
        private const int FramesPerSecond = 12;
        private const int FoodItemValue = 10;
        private const char SnakeBodyRepresentationChar = '@';
        private const char SnakeHeadRepresentationChar = '>';
        private const ConsoleColor SnakeHeadColor = ConsoleColor.Cyan;
        private const ConsoleColor SnakeBodyColor = ConsoleColor.Blue;
        private const char FoodItemRepresentationChar = '$';
        private const ConsoleColor FoodItemColor = ConsoleColor.Red;

        public void Start(IGameWindow gameWindow, IInputDevice inputDevice)
        {
            var player = new Player(new Coordinate(10, 10));
            var gameOver = false;
            var snakeDirection = Direction.Right;

            var foodItem = new FoodItem(
                FoodItemValue,
                GlobalUtilities.RandomCoordinate(gameWindow.Width - 1, gameWindow.Height - 1));


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
                Console.Write(3);

                // If player's snake touches the edges of the console gameWindow, eats itself or no more place to spawn food ..
                gameOver = false;//PlayerCollided(player.SnakeHeadCoordinate, gameWindow) || player.SnakeSelfCollided() || FoodCheck(player, ref foodItem, gameWindow);
                Console.Write(4);

                // Render
                gameWindow.Clear();
                Console.Write(5);

                foreach (var part in player.GetSnakeParts())
                {
                    gameWindow.DrawObject(
                        part.Coordinate,
                        part.IsHead ? SnakeHeadRepresentationChar : SnakeBodyRepresentationChar,
                        part.IsHead ? SnakeHeadColor : SnakeBodyColor);
                }

                gameWindow.DrawObject(foodItem.Coordinate, FoodItemRepresentationChar, FoodItemColor);

                Thread.Sleep(1000 / FramesPerSecond); // TODO calculate time frame
            }
        }

        private bool FoodCheck(Player player, ref FoodItem foodItem, IGameWindow gameWindow)
        {
            if (GlobalUtilities.MatchingCoordinates(player.SnakeHeadCoordinate, foodItem.Coordinate))
            {
                player.AteFood(foodItem.ScoreValue);
                if ((player.GetSnakeParts().Length + 1) >= (gameWindow.Height * gameWindow.Width))
                {
                    // No more room to place FoodItem, GG.
                    return true;
                }
            }

            foodItem = new FoodItem(
                    foodItem.ScoreValue,
                    GlobalUtilities.RandomCoordinate(gameWindow.Width, gameWindow.Height));
            return false;
        }

        private bool PlayerCollided(Coordinate headCoordinate, IGameWindow gameWindow)
        {
            return (headCoordinate.X >= gameWindow.Width - 1) || (headCoordinate.Y >= gameWindow.Height - 1) || (headCoordinate.X <= 0) || (headCoordinate.Y <= 0);
        }
    }
}
