namespace NunitLab.Api;

public class OrderProcessor
{
    private readonly IEmailService _emailService;
    private readonly IShippingService _shippingService;

    public OrderProcessor(
        IShippingService shippingService, 
        IEmailService emailService)
    {
        _emailService = emailService;
        _shippingService = shippingService;
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

    public decimal ProcessOrderAndShip(
        string orderId, string customerEmail,
        string destination, double weight)
    {
        if (_shippingService.ValidateShippingDetails(
            destination, weight) == false)
        { 
            throw new InvalidOperationException(
                "Invalid shipping details.");
        }

        var shippingCost = 
            _shippingService.CalculateShippingCost(
                destination, weight);

        _emailService.SendEmail(
            customerEmail, 
            "Order Confirmation", 
            $"Your order will be shipped to {destination} with a shipping cost of {shippingCost:C}.");

        return shippingCost;
    }
}
