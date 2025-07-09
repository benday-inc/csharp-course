using System;

namespace EventDrivenNotifierLab
{
    public class Logger
    {
        public void Subscribe(RegistrationService regService)
        {
            regService.UserRegistered += (s, e) =>
            {
                Console.WriteLine($"[LOG] User registered: {e.NewUser.Name} ({e.NewUser.Email})");
            };
        }
    }
}
