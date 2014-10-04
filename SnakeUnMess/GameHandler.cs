namespace SnakeUnmess
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading;

    using SnakeUnMess.Elements.Player;
    using SnakeUnMess.Elements.Snake;
    using SnakeUnMess.Interfaces;

    public static class GameHandler
    {
        private const int FramesPerSecond = 10;

        private const char SnakeBodyRepresentationChar = 'O';
        private const char SnakeHeadRepresentationChar = '@';
        private const ConsoleColor SnakeHeadColor = ConsoleColor.Green;
        private const ConsoleColor SnakeBodyColor = ConsoleColor.Green;

        private const char FoodItemRepresentationChar = '$';
        private const ConsoleColor FoodItemColor = ConsoleColor.Red;
        private const int FoodItemValue = 10;

        private static readonly Point PlayerStartingPoint = new Point(10, 10);

        public static void Start(IGameClient gameClient)
        {
            var player = new Player(PlayerStartingPoint);
            var gameOver = false;
            var gamePaused = false;

            var snakeDirection = Direction.Right;
            var foodItem = new FoodItem(new Point(0, 0), FoodItemValue);
            PlaceFood(player, ref foodItem, gameClient.GameWindow);

            var frameTimer = new Stopwatch();
            frameTimer.Start();

            // Game mainloop
            while (!gameOver)
            {
                // Handle user input using IInputDevice from ConsoleGameClient
                if (gameClient.InputDevice.KeyAvailable)
                {
                    switch (gameClient.InputDevice.PlayerRequest)
                    {
                        case PlayerRequest.Up:
                            snakeDirection = Direction.Up;
                            break;
                        case PlayerRequest.Left:
                            snakeDirection = Direction.Left;
                            break;
                        case PlayerRequest.Down:
                            snakeDirection = Direction.Down;
                            break;
                        case PlayerRequest.Right:
                            snakeDirection = Direction.Right;
                            break;
                        case PlayerRequest.Pause:
                            gamePaused = !gamePaused;
                            break;
                        case PlayerRequest.Exit:
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
                    gameOver = !(gameClient.GameWindow.Rectangle.Contains(nextMovePoint) && !player.Snake.Parts.Any(part => part.Point.Equals(nextMovePoint)));
                    if (!gameOver)
                    {
                        player.Snake.Move(nextMovePoint);

                        // If no more place to spawn food ..
                        gameOver = FoodCheck(player, ref foodItem, gameClient.GameWindow);
                    }

                    // Render
                    gameClient.GameWindow.Clear();
                    foreach (var part in player.Snake.Parts)
                    {
                        gameClient.GameWindow.DrawObject(
                            part.Point,
                            part.IsHead ? SnakeHeadRepresentationChar : SnakeBodyRepresentationChar,
                            part.IsHead ? SnakeHeadColor : SnakeBodyColor);
                    }

                    gameClient.GameWindow.DrawObject(foodItem.Point, FoodItemRepresentationChar, FoodItemColor);
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

        private static bool FoodCheck(Player player, ref FoodItem foodItem, IGameWindow gameWindow)
        {
            // If player's snake's headCoord collides with foodItem.. 
            if (player.Snake.HeadPoint.Equals(foodItem.Point))
            {
                player.AteFood(foodItem.ScoreValue);

                if ((player.Snake.Parts.Count + 1) >= (gameWindow.Height * gameWindow.Width))
                {
                    // No more room to place FoodItem
                    return true;
                }

                PlaceFood(player, ref foodItem, gameWindow);
            }

            return false;
        }

        private static void PlaceFood(Player player, ref FoodItem foodItem, IGameWindow gameWindow)
        {
            Point foodSpawnPoint;
            do
            {
                // Place given foodItem at a random point. If it collides with player's snake, repeat this process.
                foodSpawnPoint = GlobalUtilities.RandomPoint(gameWindow.Width, gameWindow.Height);
            }
            while (player.Snake.Parts.Any(part => part.Point.Equals(foodSpawnPoint)));
            foodItem = new FoodItem(foodSpawnPoint, foodItem.ScoreValue);
        }
    }
}
