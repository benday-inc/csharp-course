namespace NunitLab.Api;

public interface IEmailService
{
    void SendEmail(string recipient, string subject, string body);
}
