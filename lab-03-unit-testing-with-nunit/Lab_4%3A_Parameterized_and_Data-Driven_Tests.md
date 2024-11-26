
# Lab 4: Parameterized and Data-Driven Tests

## Objective
Learn how to use NUnit’s parameterized and data-driven test features to test multiple scenarios efficiently.

## Prerequisites
- Completion of **Lab 3** or familiarity with mocking and dependency isolation.
- Basic understanding of NUnit test attributes like `[Test]`.

## Instructions

### Step 1: Add a Discount Calculator Class
1. In the `CalculatorApp` project, create a new class `DiscountCalculator.cs`:
   ```csharp
   namespace CalculatorApp
   {
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
   }
   ```

> ![Screenshot Placeholder: DiscountCalculator implementation]

### Step 2: Write Parameterized Tests
1. In the `CalculatorApp.Tests` project, create a new test class `DiscountCalculatorTests.cs`:
   ```csharp
   using NUnit.Framework;
   using CalculatorApp;

   namespace CalculatorApp.Tests
   {
       [TestFixture]
       public class DiscountCalculatorTests
       {
           private DiscountCalculator _discountCalculator;

           [SetUp]
           public void Setup()
           {
               _discountCalculator = new DiscountCalculator();
           }

           [TestCase(100, "Regular", 5)]
           [TestCase(200, "Premium", 20)]
           [TestCase(300, "VIP", 60)]
           [TestCase(150, "Unknown", 0)]
           public void CalculateDiscount_ShouldReturnExpectedDiscount(decimal totalAmount, string membershipType, decimal expectedDiscount)
           {
               // Act
               decimal actualDiscount = _discountCalculator.CalculateDiscount(totalAmount, membershipType);

               // Assert
               Assert.AreEqual(expectedDiscount, actualDiscount);
           }
       }
   }
   ```

> ![Screenshot Placeholder: Parameterized test setup]

### Step 3: Use `[TestCaseSource]` for Complex Data Sets
1. Add a method to supply test data:
   ```csharp
   public static IEnumerable<object[]> DiscountTestCases()
   {
       yield return new object[] { 500, "Regular", 25 };
       yield return new object[] { 400, "Premium", 40 };
       yield return new object[] { 1000, "VIP", 200 };
   }
   ```

2. Use `[TestCaseSource]` to reference the data source:
   ```csharp
   [Test, TestCaseSource(nameof(DiscountTestCases))]
   public void CalculateDiscount_WithTestCaseSource_ShouldReturnExpectedDiscount(decimal totalAmount, string membershipType, decimal expectedDiscount)
   {
       // Act
       decimal actualDiscount = _discountCalculator.CalculateDiscount(totalAmount, membershipType);

       // Assert
       Assert.AreEqual(expectedDiscount, actualDiscount);
   }
   ```

> ![Screenshot Placeholder: Test methods using TestCaseSource]

### Step 4: Run the Tests
1. Open the **Test Explorer** in Visual Studio.
2. Run all tests and verify that:
   - All parameterized test cases pass.
   - Tests using `[TestCaseSource]` execute as expected.

> ![Screenshot Placeholder: Passing test results in Test Explorer]

## Outcome
Students will:
- Understand how to use `[TestCase]` for parameterized tests.
- Leverage `[TestCaseSource]` for complex or dynamically generated test data.

---
