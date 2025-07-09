using System;

namespace EventDrivenNotifierLab
{
    public class WelcomeEmailSender
    {
        public void Subscribe(RegistrationService regService)
        {
            regService.UserRegistered += (s, e) =>
            {
                Console.WriteLine($"[EMAIL] Sent welcome email to {e.NewUser.Email}");
            };
        }
    }
}
