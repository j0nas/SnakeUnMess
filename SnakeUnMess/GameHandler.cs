namespace SnakeUnMess
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    public class GameHandler
    {
        // TODO externalize into config file
        private const int FramesPerSecond = 12;
        private const int FoodItemValue = 10;
        private const char SnakeBodyRepresentationChar = 'O';
        private const char SnakeHeadRepresentationChar = '@';
        private const ConsoleColor SnakeHeadColor = ConsoleColor.Green;
        private const ConsoleColor SnakeBodyColor = ConsoleColor.Green;
        private const char FoodItemRepresentationChar = '$';
        private const ConsoleColor FoodItemColor = ConsoleColor.Red;
        private bool gamePaused = false;

        public void Start(IGameWindow gameWindow, IInputDevice inputDevice)
        {
            var player = new Player(new Coordinate(10, 10));
            var gameOver = false;
            var snakeDirection = Direction.Right;
            var foodItem = new FoodItem(
                FoodItemValue,
                GlobalUtilities.RandomCoordinate(gameWindow.Width - 1, gameWindow.Height - 1));

            var frameTimer = new Stopwatch();
            frameTimer.Start();

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
                        case UserRequest.Pause:
                            gamePaused = !gamePaused;
                            break;
                        case UserRequest.Exit:
                            this.TerminateApplication();
                            throw new Exception("Termination failed");
                    }
                }

                if (!gamePaused)
                {
                    // Change world
                    player.MoveSnake(snakeDirection);

                    // If player's snake touches the edges of the console gameWindow, eats itself or no more place to spawn food ..
                    gameOver = PlayerCollided(player.Snake.HeadCoordinate, gameWindow)
                               || FoodCheck(player, ref foodItem, gameWindow) || player.SnakeSelfCollided();

                    // Render
                    gameWindow.Clear();

                    foreach (var part in player.Snake.Parts)
                    {
                        gameWindow.DrawObject(
                            part.Coordinate,
                            part.IsHead ? SnakeHeadRepresentationChar : SnakeBodyRepresentationChar,
                            part.IsHead ? SnakeHeadColor : SnakeBodyColor);
                    }

                    gameWindow.DrawObject(foodItem.Coordinate, FoodItemRepresentationChar, FoodItemColor);
                }

                // Ensuring 100ms per frame, idle for the remainder of the timeframe.
                Thread.Sleep(Math.Max(100 - (int)frameTimer.ElapsedMilliseconds, 0));
                frameTimer.Reset();
            }
        }

        private void TerminateApplication()
        {
            Environment.Exit(exitCode: 0);
        }

        private bool FoodCheck(Player player, ref FoodItem foodItem, IGameWindow gameWindow)
        {
            if (GlobalUtilities.MatchingCoordinates(player.Snake.HeadCoordinate, foodItem.Coordinate))
            {
                player.AteFood(foodItem.ScoreValue);
                foodItem = new FoodItem(
                    foodItem.ScoreValue,
                    GlobalUtilities.RandomCoordinate(gameWindow.Width - 1, gameWindow.Height - 1));

                if ((player.Snake.Parts.Count + 1) >= (gameWindow.Height * gameWindow.Width))
                {
                    // No more room to place FoodItem, GG.
                    return true;
                }
            }

            return false;
        }

        private bool PlayerCollided(Coordinate headCoordinate, IGameWindow gameWindow)
        {
            return (headCoordinate.X >= gameWindow.Width + 1) || (headCoordinate.Y >= gameWindow.Height) || (headCoordinate.X <= 0) || (headCoordinate.Y <= 0);
        }
    }
}
