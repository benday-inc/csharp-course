using System;

namespace EventDrivenNotifierLab
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var regService = new RegistrationService();
            var logger = new Logger();
            var emailSender = new WelcomeEmailSender();

            logger.Subscribe(regService);
            emailSender.Subscribe(regService);

            regService.AdditionalAction = u => Console.WriteLine($"[CUSTOM] Additional processing for {u.Name}");

            regService.RegisterUser("Alice", "alice@example.com");
            regService.RegisterUser("Bob", "bob@example.com");
        }
    }
}
