namespace SnakeUnMess
{
    using System;

    public class ConsoleGameClient
    {
        public static void Main(String[] args)
        {
            var window = new ConsoleGameWindow();

            new GameHandler().Start(window);
        }
    }
}
