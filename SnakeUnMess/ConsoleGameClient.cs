namespace SnakeUnMess
{
    public class ConsoleGameClient
    {
        public static void Main(string[] args)
        {
            // Define GameWindow and InputDevice for the gameHandler to use and pass it
            var window = new ConsoleGameWindow();
            var inputDevice = new ConsoleInputDevice();

            new GameHandler().Start(window, inputDevice);   
        }
    }
}
