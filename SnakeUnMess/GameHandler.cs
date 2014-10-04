namespace SnakeUnmess
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading;

    using SnakeUnMess.Elements.FoodItem;
    using SnakeUnMess.Elements.Player;
    using SnakeUnMess.Elements.Snake;
    using SnakeUnMess.Global;
    using SnakeUnMess.Interfaces;

    public class GameHandler
    {
        private const int FramesPerSecond = 10;

        private const char SnakeBodyRepresentationChar = 'O'; // TODO
        private const char SnakeHeadRepresentationChar = '@'; // TODO
        private const ConsoleColor SnakeHeadColor = ConsoleColor.Green; // TODO
        private const ConsoleColor SnakeBodyColor = ConsoleColor.Green; // TODO

        private const char FoodItemRepresentationChar = '$'; // TODO
        private const ConsoleColor FoodItemColor = ConsoleColor.Red; // TODO
        private const int FoodItemValue = 10;

        private static readonly Point PlayerStartingPoint = new Point(10, 10);

        private readonly IGameClient gameClient;

        private Player player;

        private FoodItem foodItem;

        public GameHandler(IGameClient gameClient)
        {
            this.gameClient = gameClient;
        }

        public void Start()
        {
            player = new Player(PlayerStartingPoint);
            var gameOver = false;
            var gamePaused = false;

            var snakeDirection = Direction.Right;
            foodItem = new FoodItem(this.FindNewFoodPosition(), FoodItemValue);

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
                    gameOver = !(gameClient.GameWindow.Rectangle.Contains(nextMovePoint) && !player.Snake.Parts.Any(part => part.Position.Equals(nextMovePoint))); // TODO extract into methods?
                    if (!gameOver)
                    {
                        player.Snake.Move(nextMovePoint);

                        if (FoodHasBeenEaten())
                        {
                            player.Scored(foodItem.ScoreValue);

                            // If no more place to spawn food ..
                            if (SnakeFillsScreen())
                            {
                                TerminateApplication();
                            }

                            foodItem.Position = FindNewFoodPosition();
                        }
                    }

                    // Render
                    gameClient.GameWindow.Clear();
                    foreach (var part in player.Snake.Parts)
                    {
                        gameClient.GameWindow.DrawObject(
                            part.Position,
                            part.IsHead ? SnakeHeadRepresentationChar : SnakeBodyRepresentationChar,
                            part.IsHead ? SnakeHeadColor : SnakeBodyColor);
                    }

                    gameClient.GameWindow.DrawObject(foodItem.Position, FoodItemRepresentationChar, FoodItemColor);
                }

                // Ensuring idletime for framerate consistency.
                Thread.Sleep(Math.Max((1000 / FramesPerSecond) - (int)frameTimer.ElapsedMilliseconds, 0));
                frameTimer.Reset();
            }
        }

        private bool SnakeFillsScreen()
        {
            return this.player.Snake.Parts.Count + 1
                   >= this.gameClient.GameWindow.Height * this.gameClient.GameWindow.Width;
        }

        private void TerminateApplication()
        {
            Environment.Exit(0);
        }

        private bool FoodHasBeenEaten()
        {
            // If player's snake's headCoord collides with foodItem.. 
            return player.Snake.HeadPoint.Equals(foodItem.Position);
        }

        private Point FindNewFoodPosition()
        {
            Point foodSpawnPoint;
            do
            {
                // Place given foodItem at a random point. If it collides with player's snake, repeat this process.
                foodSpawnPoint = GlobalUtilities.RandomPoint(gameClient.GameWindow.Width, gameClient.GameWindow.Height);
            }
            while (player.Snake.Parts.Any(part => part.Position.Equals(foodSpawnPoint)));

            return foodSpawnPoint;
        }
    }
}
