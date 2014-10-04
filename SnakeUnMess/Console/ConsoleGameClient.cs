namespace SnakeUnMess.Console
{
    using SnakeUnMess.Interfaces;

    public class ConsoleGameClient : IGameClient
    {
        public ConsoleGameClient(IGameWindow gameWindow, IInputDevice inputDevice)
        {
            this.GameWindow = gameWindow;
            this.InputDevice = inputDevice;
        }

        public IGameWindow GameWindow { get; private set; }

        public IInputDevice InputDevice { get; private set; }
    }
}
