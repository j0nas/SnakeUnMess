namespace SnakeUnMess
{
    using SnakeUnmess;

    using SnakeUnMess.Console;

    public static class Application
    {
        public static void Main(string[] args)
        {
            // Define GameWindow and InputDevice for the gameHandler to use and pass it
            GameHandler.Start(new ConsoleGameClient(new ConsoleGameWindow(), new ConsoleInputDevice()));
        }

    }
}
