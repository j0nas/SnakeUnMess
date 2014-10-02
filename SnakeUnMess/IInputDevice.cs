namespace SnakeUnMess
{
    using System.Collections.Generic;

    public interface IInputDevice
    {
        bool KeyAvailable { get; }

        UserRequest UserRequest { get; }
    }
}
