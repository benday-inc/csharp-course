# Lab 2: Filtering with Lambda Expressions – Flexible Product Filtering

## 🧠 Goal
Use lambda expressions and `Func<T>` to write flexible, reusable filtering logic. You’ll create a simple product catalog and apply filters using lambdas.

---

## 🛠️ Step-by-Step Instructions

### ✅ Step 1: Create the Project
1. Open Visual Studio or your preferred C# development environment.
2. Create a new Console App project named `LambdaFilteringLab`.

---

### ✅ Step 2: Define the Product Class
Create a class called `Product`:

```csharp
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}
```

---

### ✅ Step 3: Seed the Product List
In `Main()`, create a list of sample products:

```csharp
List<Product> products = new List<Product>
{
    new Product { Name = "Laptop", Price = 999.99m, Category = "Electronics" },
    new Product { Name = "Desk", Price = 150.00m, Category = "Furniture" },
    new Product { Name = "Headphones", Price = 89.99m, Category = "Electronics" },
    new Product { Name = "Coffee Mug", Price = 12.50m, Category = "Kitchen" },
};
```

---

### ✅ Step 4: Implement the Filter Method
Write a method that uses a `Func<Product, bool>` parameter to determine which products to return:

```csharp
static List<Product> FilterProducts(List<Product> items, Func<Product, bool> predicate)
{
    return items.Where(predicate).ToList();
}
```

---

### ✅ Step 5: Filter with Named Method
Create a named method to use with the filter:

```csharp
static bool IsElectronics(Product p) => p.Category == "Electronics";
```

Then call:

```csharp
var electronics = FilterProducts(products, IsElectronics);
electronics.ForEach(p => Console.WriteLine(p.Name));
```

---

### ✅ Step 6: Filter with a Lambda Expression
Use a lambda directly in the filter call:

```csharp
var cheapItems = FilterProducts(products, p => p.Price < 100);
cheapItems.ForEach(p => Console.WriteLine(p.Name));
```

---

## 🌟 Stretch Goal: Dynamic Sort
### Step 7: Sort Using Lambda and `Func<Product, object>`
Add a sort method:

```csharp
static List<Product> SortProducts(List<Product> items, Func<Product, object> keySelector)
{
    return items.OrderBy(keySelector).ToList();
}
```

Example usage:

```csharp
var sortedByPrice = SortProducts(products, p => p.Price);
sortedByPrice.ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));
```

---

## ✅ What You Learned
- How to define flexible filtering logic using `Func<T, bool>`
- How to use lambda expressions for filtering and sorting
- How to create reusable functional-style utilities

---

## 🧪 Next Lab
In Lab 3, you’ll build a reactive system using events to simulate real-time stock price alerts.

