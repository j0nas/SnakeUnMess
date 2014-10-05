namespace SnakeUnMess
{
    using SnakeUnMess;

    using SnakeUnMess.Console;

    public static class Application
    {
        public static void Main(string[] args)
        {
            // Define GameWindow and InputDevice for the gameHandler to use and pass it
            new GameHandler(new ConsoleGameClient(new ConsoleGameWindow(), new ConsoleInputDevice())).Start();
        }

    }
}
