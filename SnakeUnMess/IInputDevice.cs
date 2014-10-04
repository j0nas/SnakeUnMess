namespace SnakeUnmess
{
    public interface IInputDevice
    {
        bool KeyAvailable { get; }

        UserRequest UserRequest { get; }
    }
}
