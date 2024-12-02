namespace NunitLab.Api;

public class OrderProcessor
{
    private readonly IEmailService _emailService;

    public OrderProcessor(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public bool ProcessOrder(string orderId, string customerEmail)
    {
        // Logic to process the order...
        if (string.IsNullOrEmpty(orderId)) return false;

        // Send confirmation email
        _emailService.SendEmail(
            customerEmail, 
            "Order Confirmation", 
            $"Your order {orderId} has been processed.");

        return true;
    }
}

