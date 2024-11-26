
# Lab 3: Testing Classes with Dependencies

## Objective
Learn how to test classes that depend on other services by using mocking frameworks.

## Prerequisites
- Completion of **Lab 2** or familiarity with basic NUnit tests and exception handling.
- NuGet package for Moq installed in the `CalculatorApp.Tests` project.

## Instructions

### Step 1: Create the `IEmailService` and `OrderProcessor` Classes
1. In the `CalculatorApp` project, create a new interface `IEmailService.cs`:
   ```csharp
   namespace CalculatorApp
   {
       public interface IEmailService
       {
           void SendEmail(string recipient, string subject, string body);
       }
   }
   ```

2. Create a new class `OrderProcessor.cs` that uses `IEmailService`:
   ```csharp
   namespace CalculatorApp
   {
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
               _emailService.SendEmail(customerEmail, "Order Confirmation", $"Your order {orderId} has been processed.");
               return true;
           }
       }
   }
   ```

> ![Screenshot Placeholder: Interface and OrderProcessor implementation]

### Step 2: Add Tests for `OrderProcessor`
1. In the `CalculatorApp.Tests` project, install the Moq NuGet package:
   - Right-click on the project, choose **Manage NuGet Packages**.
   - Search for and install **Moq**.

2. Create a new test class `OrderProcessorTests.cs`:
   ```csharp
   using Moq;
   using NUnit.Framework;
   using CalculatorApp;

   namespace CalculatorApp.Tests
   {
       [TestFixture]
       public class OrderProcessorTests
       {
           private Mock<IEmailService> _emailServiceMock;
           private OrderProcessor _orderProcessor;

           [SetUp]
           public void Setup()
           {
               _emailServiceMock = new Mock<IEmailService>();
               _orderProcessor = new OrderProcessor(_emailServiceMock.Object);
           }

           [Test]
           public void ProcessOrder_WhenOrderIdIsValid_ShouldSendEmail()
           {
               // Arrange
               string orderId = "12345";
               string customerEmail = "customer@example.com";

               // Act
               bool result = _orderProcessor.ProcessOrder(orderId, customerEmail);

               // Assert
               Assert.IsTrue(result);
               _emailServiceMock.Verify(
                   x => x.SendEmail(customerEmail, "Order Confirmation", $"Your order {orderId} has been processed."),
                   Times.Once);
           }

           [Test]
           public void ProcessOrder_WhenOrderIdIsInvalid_ShouldNotSendEmail()
           {
               // Arrange
               string orderId = "";
               string customerEmail = "customer@example.com";

               // Act
               bool result = _orderProcessor.ProcessOrder(orderId, customerEmail);

               // Assert
               Assert.IsFalse(result);
               _emailServiceMock.Verify(x => x.SendEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
           }
       }
   }
   ```

> ![Screenshot Placeholder: Moq setup and test methods in Visual Studio editor]

### Step 3: Run and Verify Tests
1. Open the **Test Explorer** in Visual Studio.
2. Run all tests and verify that:
   - The email is sent only when the order ID is valid.
   - No email is sent for invalid order IDs.

> ![Screenshot Placeholder: Test Explorer showing passing tests for mocked dependencies]

## Outcome
Students will:
- Understand how to use mocking frameworks to isolate dependencies in unit tests.
- Learn how to verify method calls and interactions with mocked objects.

---
