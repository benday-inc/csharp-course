
# Lab 6: Testing Asynchronous Code

## Objective
Learn how to write unit tests for asynchronous methods using NUnit.

## Prerequisites
- Completion of **Lab 5** or familiarity with custom assertions.
- Basic understanding of asynchronous programming in C# (`async` and `await`).

## Instructions

### Step 1: Create an AsyncFileProcessor Class
1. In the `CalculatorApp` project, create a new class `AsyncFileProcessor.cs`:
   ```csharp
   using System.IO;
   using System.Threading.Tasks;

   namespace CalculatorApp
   {
       public class AsyncFileProcessor
       {
           public async Task<string> ReadFileAsync(string filePath)
           {
               if (!File.Exists(filePath))
                   throw new FileNotFoundException("The file does not exist.");

               using var reader = new StreamReader(filePath);
               return await reader.ReadToEndAsync();
           }

           public async Task WriteFileAsync(string filePath, string content)
           {
               using var writer = new StreamWriter(filePath);
               await writer.WriteAsync(content);
           }
       }
   }
   ```

> ![Screenshot Placeholder: AsyncFileProcessor class implementation]

### Step 2: Write Unit Tests for AsyncFileProcessor
1. In the `CalculatorApp.Tests` project, create a new test class `AsyncFileProcessorTests.cs`:
   ```csharp
   using System.IO;
   using System.Threading.Tasks;
   using NUnit.Framework;
   using CalculatorApp;

   namespace CalculatorApp.Tests
   {
       [TestFixture]
       public class AsyncFileProcessorTests
       {
           private AsyncFileProcessor _fileProcessor;
           private string _testFilePath;

           [SetUp]
           public void Setup()
           {
               _fileProcessor = new AsyncFileProcessor();
               _testFilePath = "testFile.txt";
           }

           [TearDown]
           public void Cleanup()
           {
               if (File.Exists(_testFilePath))
                   File.Delete(_testFilePath);
           }

           [Test]
           public async Task WriteFileAsync_ShouldWriteContentToFile()
           {
               string content = "Hello, Async World!";

               await _fileProcessor.WriteFileAsync(_testFilePath, content);

               string writtenContent = File.ReadAllText(_testFilePath);
               Assert.AreEqual(content, writtenContent);
           }

           [Test]
           public async Task ReadFileAsync_ShouldReturnFileContent()
           {
               string content = "Async Read Test";
               File.WriteAllText(_testFilePath, content);

               string result = await _fileProcessor.ReadFileAsync(_testFilePath);

               Assert.AreEqual(content, result);
           }

           [Test]
           public void ReadFileAsync_FileDoesNotExist_ShouldThrowFileNotFoundException()
           {
               Assert.ThrowsAsync<FileNotFoundException>(async () => 
                   await _fileProcessor.ReadFileAsync("nonexistent.txt"));
           }
       }
   }
   ```

> ![Screenshot Placeholder: Async test methods in Visual Studio editor]

### Step 3: Run the Tests
1. Open the **Test Explorer** in Visual Studio.
2. Run all tests and verify that:
   - Writing and reading file content works correctly.
   - The appropriate exception is thrown when a file is missing.

> ![Screenshot Placeholder: Test Explorer showing passing async tests]

## Outcome
Students will:
- Learn how to write tests for asynchronous methods.
- Understand how to use `Assert.ThrowsAsync` to validate exceptions in async code.

---
