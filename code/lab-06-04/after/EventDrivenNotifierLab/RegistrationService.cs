using System;

namespace EventDrivenNotifierLab
{
    public delegate void ExtraAction(User user);

    public class RegistrationService
    {
        public event EventHandler<UserRegisteredEventArgs> UserRegistered;
        public ExtraAction AdditionalAction;

        public void RegisterUser(string name, string email)
        {
            var user = new User { Name = name, Email = email };
            Console.WriteLine($"Registering user: {name}");
            AdditionalAction?.Invoke(user);
            UserRegistered?.Invoke(this, new UserRegisteredEventArgs { NewUser = user });
        }
    }
}
