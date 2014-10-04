namespace SnakeUnMess.Interfaces
{
    public interface IGameClient
    {
        IGameWindow GameWindow { get; }

        IInputDevice InputDevice { get; }
    }
}
