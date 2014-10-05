namespace SnakeUnMess.Interfaces
{
    using SnakeUnMess.Elements.Player;

    public interface IInputDevice
    {
        bool KeyAvailable { get; }

        PlayerRequest GetPlayerRequest { get; }
    }
}
