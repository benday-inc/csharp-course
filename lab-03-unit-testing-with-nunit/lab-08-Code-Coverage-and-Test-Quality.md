
# Lab 8: Code Coverage and Test Quality

## Objective
Learn how to measure code coverage, identify gaps in testing, and improve test quality.

## Prerequisites
- Completion of **Lab 7** or familiarity with advanced mocking techniques.
- Visual Studio Enterprise or an external code coverage tool like Coverlet installed.

## Instructions

### Step 1: Enable Code Coverage in Visual Studio
1. Open Visual Studio.
2. Ensure the **Test** menu has the option **Analyze Code Coverage for All Tests**:
   - If not using Visual Studio Enterprise, install **Coverlet** via NuGet in the `CalculatorApp.Tests` project.

> ![Screenshot Placeholder: Visual Studio showing Code Coverage option]

### Step 2: Analyze Code Coverage for `BankAccount`
1. Run all tests in the `CalculatorApp.Tests` project.
2. Analyze code coverage by selecting **Test** > **Analyze Code Coverage**.
3. Review the report and identify any uncovered methods in the `BankAccount` class.

> ![Screenshot Placeholder: Code coverage report highlighting uncovered lines]

### Step 3: Add Missing Tests
1. Identify gaps in the `BankAccount` tests (e.g., handling negative or zero amounts in `Deposit` and `Withdraw` methods).
2. Add the following tests to `BankAccountTests.cs`:
   ```csharp
   [Test]
   public void Deposit_WhenAmountIsNegative_ShouldThrowArgumentException()
   {
       Assert.Throws<ArgumentException>(() => _account.Deposit(-50));
   }

   [Test]
   public void Withdraw_WhenAmountIsGreaterThanBalance_ShouldThrowInvalidOperationException()
   {
       Assert.Throws<InvalidOperationException>(() => _account.Withdraw(200));
   }
   ```

3. Re-run tests and verify the code coverage report shows improved coverage.

> ![Screenshot Placeholder: Updated code coverage report with higher coverage percentage]

### Step 4: Evaluate Test Quality
1. Use the following checklist to assess test quality:
   - Do tests cover all paths (happy and edge cases)?
   - Are tests clearly named to describe their intent?
   - Do tests validate all expected outcomes (e.g., results, exceptions)?

2. Refactor any poorly written tests for readability or maintainability.

> ![Screenshot Placeholder: Checklist for test quality evaluation]

### Step 5: Optimize Test Coverage
1. Add `[ExcludeFromCodeCoverage]` attribute to methods that do not need testing (e.g., logging or trivial getters/setters):
   ```csharp
   [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
   public void Log(string message)
   {
       Console.WriteLine(message);
   }
   ```

2. Re-run the coverage analysis to focus on meaningful improvements.

> ![Screenshot Placeholder: Excluded methods in coverage report]

## Outcome
Students will:
- Understand how to measure and improve code coverage.
- Identify gaps in test suites and write additional tests to achieve high-quality coverage.

---
