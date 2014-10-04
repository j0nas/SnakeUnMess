namespace SnakeUnmess
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading;

    public class GameHandler
    {
        // TODO externalize into config file
        private const int FramesPerSecond = 10;
        private const int FoodItemValue = 10;
        private const char SnakeBodyRepresentationChar = 'O';
        private const char SnakeHeadRepresentationChar = '@';
        private const ConsoleColor SnakeHeadColor = ConsoleColor.Green;
        private const ConsoleColor SnakeBodyColor = ConsoleColor.Green;
        private const char FoodItemRepresentationChar = '$';
        private const ConsoleColor FoodItemColor = ConsoleColor.Red;
        private bool gamePaused;

        public void Start(IGameWindow gameWindow, IInputDevice inputDevice)
        {
            var player = new Player(new Point(10, 10));
            var gameOver = false;
            var snakeDirection = Direction.Right;
            var foodItem = new FoodItem(GlobalUtilities.RandomPoint(gameWindow.Width, gameWindow.Height), FoodItemValue);
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
                            TerminateApplication();
                            throw new Exception("Termination failed");
                    }
                }

                if (!gamePaused)
                {
                    // Change world                    
                    var nextMovePoint = player.Snake.NextMovePoint(snakeDirection);

                    // If next move is invalid because Snake either touches edges or itself ..
                    // (Reverse &&s instead of ||s because && does not evaluate remainder of statement if part of evaluated statement is false)                    
                    gameOver = !(gameWindow.Rectangle.Contains(nextMovePoint) && !player.Snake.Parts.Any(part => part.Point.Equals(nextMovePoint)));
                    if (!gameOver)
                    {
                        player.Snake.Move(nextMovePoint);

                        // If no more place to spawn food ..
                        gameOver = FoodCheck(player, ref foodItem, gameWindow);
                    }

                    // Render
                    gameWindow.Clear();
                    foreach (var part in player.Snake.Parts)
                    {
                        gameWindow.DrawObject(
                            part.Point,
                            part.IsHead ? SnakeHeadRepresentationChar : SnakeBodyRepresentationChar,
                            part.IsHead ? SnakeHeadColor : SnakeBodyColor);
                    }

                    gameWindow.DrawObject(foodItem.Point, FoodItemRepresentationChar, FoodItemColor);
                }

                // Ensuring idletime for framerate consistency.
                Thread.Sleep(Math.Max((1000 / FramesPerSecond) - (int)frameTimer.ElapsedMilliseconds, 0));
                frameTimer.Reset();
            }
        }

        private static void TerminateApplication()
        {
            Environment.Exit(exitCode: 0);
        }

        private bool FoodCheck(Player player, ref FoodItem foodItem, IGameWindow gameWindow)
        {
            if (player.Snake.HeadPoint.Equals(foodItem.Point))
            {
                player.AteFood(foodItem.ScoreValue);
                foodItem = new FoodItem(GlobalUtilities.RandomPoint(gameWindow.Width, gameWindow.Height), foodItem.ScoreValue);

                if ((player.Snake.Parts.Count + 1) >= (gameWindow.Height * gameWindow.Width))
                {
                    // No more room to place FoodItem
                    return true;
                }
            }

            return false;
        }
    }
}
