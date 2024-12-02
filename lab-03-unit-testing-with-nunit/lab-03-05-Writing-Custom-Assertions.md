
# Lab 5: Writing Custom Assertions

## Objective
Learn how to create and use custom assertions to improve test clarity and reuse.

## Prerequisites
- Completion of **Lab 4** or familiarity with parameterized tests.
- Basic understanding of NUnit and assertions.

## Instructions

### Step 1: Create the BankAccount Class
1. In the `CalculatorApp` project, create a new class `BankAccount.cs`:
   ```csharp
   namespace CalculatorApp
   {
       public class BankAccount
       {
           public decimal Balance { get; private set; }

           public BankAccount(decimal initialBalance)
           {
               if (initialBalance < 0)
                   throw new ArgumentException("Initial balance cannot be negative.");

               Balance = initialBalance;
           }

           public void Deposit(decimal amount)
           {
               if (amount <= 0)
                   throw new ArgumentException("Deposit amount must be positive.");

               Balance += amount;
           }

           public void Withdraw(decimal amount)
           {
               if (amount <= 0)
                   throw new ArgumentException("Withdrawal amount must be positive.");
               if (amount > Balance)
                   throw new InvalidOperationException("Insufficient funds.");

               Balance -= amount;
           }
       }
   }
   ```

> ![Screenshot Placeholder: BankAccount class implementation]

### Step 2: Add Tests for BankAccount
1. In the `CalculatorApp.Tests` project, create a new test class `BankAccountTests.cs`:
   ```csharp
   using NUnit.Framework;
   using CalculatorApp;

   namespace CalculatorApp.Tests
   {
       [TestFixture]
       public class BankAccountTests
       {
           private BankAccount _account;

           [SetUp]
           public void Setup()
           {
               _account = new BankAccount(100);
           }

           [Test]
           public void Deposit_ShouldIncreaseBalance()
           {
               _account.Deposit(50);
               Assert.AreEqual(150, _account.Balance);
           }

           [Test]
           public void Withdraw_ShouldDecreaseBalance()
           {
               _account.Withdraw(30);
               Assert.AreEqual(70, _account.Balance);
           }
       }
   }
   ```

> ![Screenshot Placeholder: Basic BankAccount test methods]

### Step 3: Write Custom Assertions
1. Create a helper class `BankAccountAssertions.cs` in the `CalculatorApp.Tests` project:
   ```csharp
   using NUnit.Framework;

   namespace CalculatorApp.Tests
   {
       public static class BankAccountAssertions
       {
           public static void AssertBalanceIsNonNegative(decimal balance)
           {
               Assert.GreaterThanOrEqual(balance, 0, "Balance should be non-negative.");
           }

           public static void AssertBalanceIsWithinLimits(decimal balance, decimal upperLimit)
           {
               Assert.LessThanOrEqual(balance, upperLimit, $"Balance should not exceed {upperLimit}.");
           }
       }
   }
   ```

2. Use these custom assertions in `BankAccountTests.cs`:
   ```csharp
   [Test]
   public void Withdraw_ShouldLeaveNonNegativeBalance()
   {
       _account.Withdraw(100);
       BankAccountAssertions.AssertBalanceIsNonNegative(_account.Balance);
   }

   [Test]
   public void Deposit_ShouldNotExceedUpperLimit()
   {
       decimal upperLimit = 1000;
       _account.Deposit(900);
       BankAccountAssertions.AssertBalanceIsWithinLimits(_account.Balance, upperLimit);
   }
   ```

> ![Screenshot Placeholder: Tests using custom assertions]

### Step 4: Run the Tests
1. Open the **Test Explorer** in Visual Studio.
2. Run all tests and verify that the custom assertions work as expected.

> ![Screenshot Placeholder: Test Explorer showing passing tests with custom assertions]

## Outcome
Students will:
- Understand how to write reusable assertion methods.
- Learn how custom assertions can simplify and enhance test readability.

---
