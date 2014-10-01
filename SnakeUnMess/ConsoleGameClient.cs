namespace SnakeUnMess
{
    using System;

    public class ConsoleGameClient
    {
        public static void Main(String[] args)
        {
            var window = new ConsoleGameWindow();
            var inputDevice = new ConsoleInputDevice();

            new GameHandler().Start(window, inputDevice);
        }
    }
}
