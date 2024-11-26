
# Lab 7: Advanced Mocking with Callbacks

## Objective
Learn how to use advanced mocking techniques, such as callbacks, to test classes that involve complex interactions.

## Prerequisites
- Completion of **Lab 6** or familiarity with testing asynchronous code.
- Familiarity with mocking frameworks like Moq.

## Instructions

### Step 1: Create an IShippingService and OrderProcessor Classes
1. In the `CalculatorApp` project, update or create an interface `IShippingService.cs`:
   ```csharp
   namespace CalculatorApp
   {
       public interface IShippingService
       {
           decimal CalculateShippingCost(string destination, double weight);
       }
   }
   ```

2. Extend the `OrderProcessor` class to use the `IShippingService`:
   ```csharp
   namespace CalculatorApp
   {
       public class OrderProcessor
       {
           private readonly IShippingService _shippingService;
           private readonly IEmailService _emailService;

           public OrderProcessor(IShippingService shippingService, IEmailService emailService)
           {
               _shippingService = shippingService;
               _emailService = emailService;
           }

           public decimal ProcessOrder(string destination, double weight, string customerEmail)
           {
               if (string.IsNullOrEmpty(destination) || weight <= 0)
                   throw new ArgumentException("Invalid order details.");

               decimal shippingCost = _shippingService.CalculateShippingCost(destination, weight);
               _emailService.SendEmail(customerEmail, "Order Confirmation", $"Your order will be shipped to {destination} with a shipping cost of {shippingCost:C}.");

               return shippingCost;
           }
       }
   }
   ```

> ![Screenshot Placeholder: Updated OrderProcessor with IShippingService]

### Step 2: Write Tests with Callback Mocking
1. In the `CalculatorApp.Tests` project, update the `OrderProcessorTests.cs` file:
   ```csharp
   using Moq;
   using NUnit.Framework;
   using CalculatorApp;

   namespace CalculatorApp.Tests
   {
       [TestFixture]
       public class OrderProcessorTests
       {
           private Mock<IShippingService> _shippingServiceMock;
           private Mock<IEmailService> _emailServiceMock;
           private OrderProcessor _orderProcessor;

           [SetUp]
           public void Setup()
           {
               _shippingServiceMock = new Mock<IShippingService>();
               _emailServiceMock = new Mock<IEmailService>();
               _orderProcessor = new OrderProcessor(_shippingServiceMock.Object, _emailServiceMock.Object);
           }

           [Test]
           public void ProcessOrder_ShouldCallCalculateShippingCost_WithCorrectParameters()
           {
               // Arrange
               string destination = "New York";
               double weight = 5.0;
               decimal expectedShippingCost = 10.0m;

               _shippingServiceMock.Setup(s => s.CalculateShippingCost(destination, weight))
                   .Returns(expectedShippingCost);

               // Act
               decimal actualShippingCost = _orderProcessor.ProcessOrder(destination, weight, "customer@example.com");

               // Assert
               Assert.AreEqual(expectedShippingCost, actualShippingCost);
               _shippingServiceMock.Verify(s => s.CalculateShippingCost(destination, weight), Times.Once);
           }

           [Test]
           public void ProcessOrder_ShouldSendEmail_WithShippingDetails()
           {
               // Arrange
               string destination = "California";
               double weight = 2.5;
               decimal shippingCost = 15.0m;
               string email = "customer@example.com";

               _shippingServiceMock.Setup(s => s.CalculateShippingCost(destination, weight))
                   .Returns(shippingCost);

               _emailServiceMock.Setup(e => e.SendEmail(email, It.IsAny<string>(), It.IsAny<string>()))
                   .Callback<string, string, string>((recipient, subject, body) =>
                   {
                       Assert.AreEqual(email, recipient);
                       Assert.IsTrue(body.Contains($"shipping cost of {shippingCost:C}"));
                   });

               // Act
               _orderProcessor.ProcessOrder(destination, weight, email);

               // Assert
               _emailServiceMock.Verify(e => e.SendEmail(email, It.IsAny<string>(), It.IsAny<string>()), Times.Once);
           }
       }
   }
   ```

> ![Screenshot Placeholder: Callback mock tests in Visual Studio editor]

### Step 3: Run the Tests
1. Open the **Test Explorer** in Visual Studio.
2. Run all tests and verify that:
   - `CalculateShippingCost` is called with the correct parameters.
   - Email content is validated through a callback.

> ![Screenshot Placeholder: Test Explorer showing passing tests for callbacks]

## Outcome
Students will:
- Understand how to use callbacks in mocking frameworks to validate dynamic interactions.
- Learn to test classes with complex dependencies and interactions.

---
