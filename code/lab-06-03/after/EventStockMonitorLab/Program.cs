using System;

public class PriceChangedEventArgs : EventArgs
{
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
}

public class Stock
{
    public string Symbol { get; set; }
    private decimal price;

    public event EventHandler<PriceChangedEventArgs> PriceChanged;

    public decimal Price
    {
        get => price;
        set
        {
            if (price != value)
            {
                var oldPrice = price;
                price = value;
                PriceChanged?.Invoke(this, new PriceChangedEventArgs { OldPrice = oldPrice, NewPrice = value });
            }
        }
    }
}

public class StockMonitor
{
    public void Subscribe(Stock stock)
    {
        stock.PriceChanged += HandlePriceChanged;
    }

    private void HandlePriceChanged(object sender, PriceChangedEventArgs e)
    {
        Console.WriteLine($"Stock price changed from {e.OldPrice:C} to {e.NewPrice:C}");
    }
}

class Program
{
    static void Main()
    {
        var stock = new Stock { Symbol = "ACME", Price = 100.0m };
        var monitor = new StockMonitor();
        monitor.Subscribe(stock);

        // Stretch goal: also subscribe using lambda
        stock.PriceChanged += (s, e) =>
            Console.WriteLine($"[Lambda] Price: {e.OldPrice:C} → {e.NewPrice:C}");

        stock.Price = 101.0m;
        stock.Price = 105.5m;
        stock.Price = 105.5m; // Should not raise event
        stock.Price = 99.9m;
    }
}
