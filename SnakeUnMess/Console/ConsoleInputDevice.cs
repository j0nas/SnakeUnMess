namespace SnakeUnMess.Console
{
    using System;
    using System.Drawing.Printing;

    using SnakeUnMess.Elements.Player;
    using SnakeUnMess.Interfaces;

    public class ConsoleInputDevice : IInputDevice
    {
        public bool KeyAvailable
        {
            get
            {
                return Console.KeyAvailable;
            }
        }

        public PlayerRequest PlayerRequest
        {
            get
            {
                var key = Console.ReadKey(true).Key;

                switch (key)
                {
                    // case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.NumPad8:
                        return PlayerRequest.Up;
                    // case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.NumPad4:
                        return PlayerRequest.Left;
                    // case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.NumPad5:
                        return PlayerRequest.Down;
                    // case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.NumPad6:
                        return PlayerRequest.Right;
                    case ConsoleKey.Escape:
                        return PlayerRequest.Exit;
                    case ConsoleKey.Spacebar:
                        return PlayerRequest.Pause;
                    default:
                        return PlayerRequest.Nothing;
                }
            }
        }
    }
}
