namespace SnakeUnmess
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;

    using SnakeUnMess.Elements;
    using SnakeUnMess.Elements.FoodItem;
    using SnakeUnMess.Elements.Player;
    using SnakeUnMess.Elements.Snake;
    using SnakeUnMess.Global;
    using SnakeUnMess.Interfaces;

    public class GameHandler
    {
        private const int FramesPerSecond = 10;

        private const int FoodItemValue = 10;

        private static readonly Point PlayerStartingPoint = new Point(10, 10);

        private readonly IGameClient gameClient;

        private Player player;

        private FoodItem foodItem;

        private Direction snakeDirection = Direction.Right;

        private bool gamePaused;

        public GameHandler(IGameClient gameClient)
        {
            this.gameClient = gameClient;
        }

        public void Start()
        {
            player = new Player(PlayerStartingPoint);
            foodItem = new FoodItem(FindNewFoodPosition(), FoodItemValue);
            var gameOver = false;
            var frameTimer = new Stopwatch();
            frameTimer.Start();

            // Game mainloop
            while (!gameOver)
            {
                HandlePlayerInput();

                // Ensuring idletime for framerate consistency.
                if (!(!gamePaused && !((int)frameTimer.ElapsedMilliseconds < 1000 / FramesPerSecond)))
                {
                    continue;
                }

                frameTimer.Restart();

                // Change world                    
                gameOver = SnakeMovedLegally();
                RenderElements();
            }
        }

        private bool SnakeMovedLegally()
        {
            var nextPosition = player.Snake.NextMovePoint(snakeDirection);

            // If next move is invalid because Snake either touches edges or itself ..
            // (Reverse &&s instead of ||s because && does not evaluate remainder of statement if part of evaluated statement is false)                    
            var gameOver = !(PointIsWithinBounds(nextPosition) && !SnakeIsCannibal(nextPosition));

            if (!gameOver)
            {
                player.Snake.Move(nextPosition);
                HandleFoodEating();
            }

            return gameOver;
        }

        private void HandleFoodEating()
        {
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

        private void RenderElements()
        {
            gameClient.GameWindow.Clear();
            foreach (var part in player.Snake.Parts)
            {
                gameClient.GameWindow.DrawObject(part.Position, part.Type);
            }

            gameClient.GameWindow.DrawObject(foodItem.Position, GameObjectType.Food);
        }

        private bool PointIsWithinBounds(Point point)
        {
            return gameClient.GameWindow.Rectangle.Contains(point);
        }

        private bool SnakeIsCannibal(Point point)
        {
            return player.Snake.Parts.Any(part => part.Position.Equals(point));
        }

        private void HandlePlayerInput()
        {
            if (gameClient.InputDevice.KeyAvailable)
            {
                switch (gameClient.InputDevice.PlayerRequest)
                {
                    case PlayerRequest.Up:
                        this.snakeDirection = Direction.Up;
                        break;
                    case PlayerRequest.Left:
                        this.snakeDirection = Direction.Left;
                        break;
                    case PlayerRequest.Down:
                        this.snakeDirection = Direction.Down;
                        break;
                    case PlayerRequest.Right:
                        this.snakeDirection = Direction.Right;
                        break;
                    case PlayerRequest.Pause:
                        gamePaused = !gamePaused;
                        break;
                    case PlayerRequest.Exit:
                        TerminateApplication();
                        throw new Exception("Termination failed");
                }
            }
        }

        private bool SnakeFillsScreen()
        {
            return player.Snake.Parts.Count + 1 >= gameClient.GameWindow.Height * gameClient.GameWindow.Width;
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
