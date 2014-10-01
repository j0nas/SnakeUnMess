namespace SnakeUnMess
{
    using System.Collections.Generic;

    public interface IInputDevice
    {
        bool KeyAvailable { get; set; }

        UserRequest UserRequest { get; set;  }
    }
}
