namespace NunitLab.Api;

public interface IShippingService
{
    decimal CalculateShippingCost(string destination, double weight);
    bool ValidateShippingDetails(string destination, double weight);
}