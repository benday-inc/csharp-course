using System;

namespace EventDrivenNotifierLab
{
    public class UserRegisteredEventArgs : EventArgs
    {
        public User NewUser { get; set; }
    }
}
