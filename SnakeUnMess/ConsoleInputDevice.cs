﻿namespace SnakeUnMess
{
    using System;

    public class ConsoleInputDevice : IInputDevice
    {
        public bool KeyAvailable
        {
            get
            {
                return Console.KeyAvailable;
            }

            set
            {
                throw new Exception("Fuck you");
            }
        }

        public UserRequest UserRequest
        {
            get
            {
                var key = Console.ReadKey(false).Key;

                switch (key)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.NumPad8:
                        return UserRequest.Up;
                    case ConsoleKey.A:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.NumPad4:
                        return UserRequest.Left;
                    case ConsoleKey.S:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.NumPad5:
                        return UserRequest.Down;
                    case ConsoleKey.D:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.NumPad6:
                        return UserRequest.Right;
                    default:
                        return UserRequest.Nothing;
                }
            }

            set
            {
                throw new Exception("Fuck you");
            }
        }
    }
}