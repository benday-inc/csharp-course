
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

<img src="/Users/benday/code/benday-inc/csharp-course/lab-03-unit-testing-with-nunit/image-20241202143441661-3174792.png" alt="image-20241202143441661" style="zoom:50%;" />


### Step 3: Write Unit Tests
1. In the `NunitLab.UnitTests` project, create a new test class `CalculatorTests.cs`:

```csharp
using NunitLab.Api;

namespace NunitLab.UnitTests;

[TestFixture]
public class CalculatorTests
{
    private Calculator? _systemUnderTest;

    public Calculator SystemUnderTest
    {
        get
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = new Calculator();
            }
    
            Assert.That(_systemUnderTest, Is.Not.Null);
    
            return _systemUnderTest;
        }
    }
    
    [TearDown]
    public void TearDown()
    {
        _systemUnderTest = null;
    }
    
    [Test]
    public void Add_ShouldReturnCorrectSum()
    {
        // arrange 
        var value1 = 2;
        var value2 = 3;
        var expected = 5;
    
        // act
        var actual = SystemUnderTest.Add(value1, value2);
    
        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void Subtract_ShouldReturnCorrectDifference()
    {
        // arrange 
        var value1 = 2;
        var value2 = 3;
        var expected = -1;
    
        // act
        var actual = SystemUnderTest.Subtract(value1, value2);
    
        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void Multiply_ShouldReturnCorrectProduct()
    {
        // arrange 
        var value1 = 2;
        var value2 = 3;
        var expected = 6;
    
        // act
        var actual = SystemUnderTest.Multiply(value1, value2);
    
        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
    
    [Test]
    public void Divide_ShouldReturnCorrectQuotient()
    {
        // arrange 
        var value1 = 6;
        var value2 = 3;
        var expected = 2;
    
        // act
        var actual = SystemUnderTest.Divide(value1, value2);
    
        // assert
        Assert.That(actual, Is.EqualTo(expected));
    }
}
```

The code should look something like the screenshot below.
<img src="/Users/benday/code/benday-inc/csharp-course/lab-03-unit-testing-with-nunit/image-20241202150614158.png" alt="image-20241202150614158" style="zoom:50%;" />

### Step 4: Try to Compile 

1. From the main menu, choose **Build > Build Solution...**

It won't compile because there's no implementation

### Step 5: Implement Enough to Compile

Let's get the compilation working

1. In the `NunitLab.Api` project, create a class called `Calculator.cs`:
   ```csharp
   namespace NunitLab.Api
   {
       public class Calculator
       {
           public int Add(int a, int b) => default;
           public int Subtract(int a, int b) => default;
           public int Multiply(int a, int b) => default;
           public int Divide(int a, int b) => default;
       }
   }
   ```

   NOTE: you could also make these methods `throw new NotImplementedException()` if you wanted

### Step 8: Run the Tests
1. Open the **Test Explorer** in Visual Studio:
   - Go to **Test** > **Test Explorer**.
2. Build the solution, and your tests will appear in the **Test Explorer**.
3. Run all tests and verify they (hopefully) fail.

<img src="/Users/benday/code/benday-inc/csharp-course/lab-03-unit-testing-with-nunit/image-20241202162448708.png" alt="image-20241202162448708" style="zoom:50%;" />

### Step 9: Implement Enough to Make the Tests Pass
1. In the `NunitLab.Api` project, create a class called `Calculator.cs`:
   ```csharp
   namespace NunitLab.Api
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
### Step 10: Run the Tests
1. Open the **Test Explorer** in Visual Studio:
   - Go to **Test** > **Test Explorer**.
2. Build the solution, and your tests will appear in the **Test Explorer**.
3. Run all tests and verify they (hopefully) pass.

<img src="/Users/benday/code/benday-inc/csharp-course/lab-03-unit-testing-with-nunit/image-20241202162605339.png" alt="image-20241202162605339" style="zoom:50%;" />



---
