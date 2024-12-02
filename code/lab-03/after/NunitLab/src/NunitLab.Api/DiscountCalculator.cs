namespace NunitLab.Api;

public class DiscountCalculator
{
    public decimal CalculateDiscount(decimal totalAmount, string membershipType)
    {
        return membershipType switch
        {
            "Regular" => totalAmount * 0.05m,
            "Premium" => totalAmount * 0.10m,
            "VIP" => totalAmount * 0.20m,
            _ => 0m
        };
    }
}

