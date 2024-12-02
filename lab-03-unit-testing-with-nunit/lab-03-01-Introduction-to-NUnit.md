
# Lab 1: Introduction to NUnit

## Objective
Set up a basic NUnit project and write simple tests.

## Prerequisites
- Visual Studio (2019 or later) installed.
- Basic understanding of C# and object-oriented programming.

## Instructions

### Step 1: Set Up the Project
1. Open Visual Studio and create a new **Class Library (.NET)** project.
2. Name the project `NunitLab`.
3. Add a second project to the solution for unit tests:
   - Right-click on the solution in **Solution Explorer**, choose **Add** > **New Project**.
   - Select **Unit Test Project (.NET)** and name it `NunitLab.UnitTests`.
4. Add a reference to the main project from the test project:
   - Right-click on `NunitLab.UnitTests`, choose **Add** > **Project Reference**.
   - Select `NunitLab` and click **OK**.

### Step 2: Install NUnit
1. In the `NunitLab.UnitTests` project, open the **NuGet Package Manager**:
   - Right-click on the project, choose **Manage NuGet Packages**.
2. Search for and install the following packages:
   - **NUnit**
   - **NUnit3TestAdapter**

<img src="/Users/benday/code/benday-inc/csharp-course/lab-03-unit-testing-with-nunit/image-20241202143441661.png" alt="image-20241202143441661" style="zoom:50%;" />

### Step 3: Implement a Simple Calculator
1. In the `NunitLab` project, create a class called `Calculator.cs`:
   ```csharp
   namespace NunitLab
   {
       public class Calculator
       {
           public int Add(int a, int b) => a + b;
           public int Subtract(int a, int b) => a - b;
           public int Multiply(int a, int b) => a * b;
           public int Divide(int a, int b) => a / b;
       }
   }
   ```

### Step 4: Write Unit Tests
1. In the `NunitLab.UnitTests` project, create a new test class `CalculatorTests.cs`:
   ```csharp
   using NUnit.Framework;
   using NunitLab;
   
   namespace NunitLab.UnitTests
   {
       [TestFixture]
       public class CalculatorTests
       {
           private Calculator _calculator;
   
           [SetUp]
           public void Setup()
           {
               _calculator = new Calculator();
           }
   
           [Test]
           public void Add_ShouldReturnCorrectSum()
           {
               Assert.AreEqual(5, _calculator.Add(2, 3));
           }
   
           [Test]
           public void Subtract_ShouldReturnCorrectDifference()
           {
               Assert.AreEqual(1, _calculator.Subtract(3, 2));
           }
   
           [Test]
           public void Multiply_ShouldReturnCorrectProduct()
           {
               Assert.AreEqual(6, _calculator.Multiply(2, 3));
           }
   
           [Test]
           public void Divide_ShouldReturnCorrectQuotient()
           {
               Assert.AreEqual(2, _calculator.Divide(6, 3));
           }
       }
   }
   ```

> ![Screenshot Placeholder: Test methods in Visual Studio editor]

### Step 5: Run the Tests
1. Open the **Test Explorer** in Visual Studio:
   - Go to **Test** > **Test Explorer**.
2. Build the solution, and your tests will appear in the **Test Explorer**.
3. Run all tests and verify they pass.

> ![Screenshot Placeholder: Test Explorer showing successful test run]

## Outcome
Students will have a basic NUnit test project, understand `[Test]` and `[SetUp]` attributes, and know how to run tests.

---
