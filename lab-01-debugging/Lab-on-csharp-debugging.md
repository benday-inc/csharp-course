# Hands-On Lab: Debugging with Breakpoints in Visual Studio 2022

## Lab Overview
In this lab, you will debug a multithreaded C# application that contains a subtle bug caused by a rogue static variable. You will explore various types of breakpoints available in Visual Studio 2022 to pinpoint and diagnose the issue systematically. By the end of this lab, you will have hands-on experience using line breakpoints, conditional breakpoints, hit count breakpoints, data breakpoints, function breakpoints, and tracepoints.

### Prerequisites
- Visual Studio 2022 installed
- Basic knowledge of C# and debugging

—

## Step 1: Setting Up the Project

1. Open Visual Studio 2022.
2. Create a new **Console App** project.
3. Replace the contents of `Program.cs` with the following code:

   ```csharp
   using System;
   using System.Collections.Concurrent;
   using System.Linq;
   using System.Threading;
   using System.Threading.Tasks;

   class Program
   {
       static void Main(string[] args)
       {
           Console.WriteLine(“Simulating user-specific data processing...”);

           var tasks = new[]
           {
               Task.Run(() => SimulateUserProcessing(“User1”, new[] { “ItemA”, “ItemB” })),
               Task.Run(() => SimulateUserProcessing(“User2”, new[] { “ItemX”, “ItemY” }))
           };

           Task.WaitAll(tasks);

           Console.WriteLine(“Simulation complete.”);
       }

       static void SimulateUserProcessing(string userId, string[] items)
       {
           Console.WriteLine($”[{Thread.CurrentThread.ManagedThreadId}] Starting processing for {userId}”);

           var processor = new DataProcessor(userId);
           processor.ProcessData(items);

           Thread.Sleep(new Random().Next(10, 100));

           var processedItems = processor.GetProcessedData();
           Console.WriteLine($”[{Thread.CurrentThread.ManagedThreadId}] {userId}’s Processed Data: {string.Join(“, “, processedItems)}”);
       }
   }

   class DataProcessor
   {
       private readonly string _userId;
       private static readonly Lazy<ConcurrentDictionary<string, string[]>> _cache =
           new(() => new ConcurrentDictionary<string, string[]>());

       public DataProcessor(string userId)
       {
           _userId = userId;
       }

       public void ProcessData(string[] data)
       {
           _cache.Value[_userId] = data.Select(d => $”{_userId}-{d}”).ToArray();
       }

       public string[] GetProcessedData()
       {
           if (_cache.Value.TryGetValue(_userId, out var processedData))
           {
               return processedData;
           }
           Console.WriteLine($”[{Thread.CurrentThread.ManagedThreadId}] Warning: No data found for {_userId}. Returning empty array.”);
           return Array.Empty<string>();
       }
   }
   ```

4. Build the project to ensure there are no syntax errors.

### Add Screenshot:
- Take a screenshot of the Visual Studio code editor with the provided code pasted into `Program.cs`.

—

## Step 2: Running the Application

1. Run the application by pressing `F5` or selecting **Debug > Start Debugging**.
2. Observe the output. You should see:

   - Both users’ processed data includes items from both users.
   - Example output:

     ```
     Simulating user-specific data processing...
     [4] Starting processing for User1
     [5] Starting processing for User2
     Initializing shared cache.
     [4] User1’s Processed Data: User1-ItemX, User1-ItemY
     [5] User2’s Processed Data: User1-ItemX, User1-ItemY
     Simulation complete.
     ```

3. Note that the bug is caused by the shared static `_cache`. You will now debug this step-by-step using different breakpoint types.

### Add Screenshot:
- Take a screenshot of the program’s console output showing the incorrect behavior.

—

## Step 3: Debugging with Breakpoints

### Task 1: Line Breakpoint
1. Set a line breakpoint on the following line in the `ProcessData` method:
   ```csharp
   _cache.Value[_userId] = data.Select(d => $”{_userId}-{d}”).ToArray();
   ```
2. Run the program in debug mode (`F5`).
3. When the breakpoint hits, inspect:
   - The value of `_userId`.
   - The contents of `data`.

### Add Screenshot:
- Take a screenshot of the debugger showing the breakpoint hit, with the Locals window open displaying `_userId` and `data`.

—

### Task 2: Conditional Breakpoint
1. Remove the previous breakpoint.
2. Set a conditional breakpoint on the same line:
   - Right-click the line number > **Conditions...** > Add condition: `_userId == “User1” && _cache.Value.ContainsKey(“User2”)`.
3. Run the program in debug mode.
4. Observe when the breakpoint hits and inspect the state of `_cache`.

### Add Screenshot:
- Take a screenshot of the conditional breakpoint setup window.
- Take another screenshot of the debugger showing `_cache` contents.

—

### Task 3: Hit Count Breakpoint
1. Remove the conditional breakpoint.
2. Add a new line breakpoint on the same line.
3. Right-click the breakpoint > **Hit Count...** > Select “Break when the hit count is” > Set to `2`.
4. Run the program in debug mode.
5. The breakpoint will now hit on the second user’s operation.

### Add Screenshot:
- Take a screenshot of the hit count breakpoint setup window.
- Take another screenshot of the debugger when the breakpoint is hit.

—

### Task 4: Data Breakpoint
1. Remove all previous breakpoints.
2. Add a data breakpoint on `_cache.Value`:
   - Run the program in debug mode.
   - Open the **Watch** window, add `_cache.Value` to it, and right-click > **Break when value changes**.
3. Observe when the cache is modified.

### Add Screenshot:
- Take a screenshot of the Watch window showing the data breakpoint setup.

—

### Task 5: Tracepoint
1. Remove all previous breakpoints.
2. Set a tracepoint on the following line in the `SimulateUserProcessing` method:
   ```csharp
   Console.WriteLine($”[{Thread.CurrentThread.ManagedThreadId}] {userId}’s Processed Data: {string.Join(“, “, processedItems)}”);
   ```
3. Right-click the breakpoint > **Actions...** > Add the following message:
   - “User: {userId}, Thread: {Thread.CurrentThread.ManagedThreadId}, Processed: {string.Join(“, “, processedItems)}”
4. Check “Continue execution” to avoid stopping at the tracepoint.
5. Run the program in debug mode.

### Add Screenshot:
- Take a screenshot of the tracepoint setup window.
- Take another screenshot of the console output showing tracepoint logs.

—

## Step 4: Diagnosing the Bug
Using the breakpoints above, identify that:

1. `_cache` is a static variable shared across all `DataProcessor` instances.
2. Each user’s data is overwriting the other’s in the shared cache.

—

## Step 5: Fixing the Bug

1. Replace the static cache with an instance-level dictionary:

   ```csharp
   private readonly ConcurrentDictionary<string, string[]> _cache = new();
   ```

2. Update `ProcessData` and `GetProcessedData` to use the instance-level `_cache`.
3. Re-run the application to confirm the bug is fixed.

### Add Screenshot:
- Take a screenshot of the fixed code.
- Take another screenshot of the corrected program output.

—

## Summary
In this lab, you:
- Used various types of breakpoints to debug a multithreaded application.
- Diagnosed a bug caused by a rogue static variable.
- Fixed the bug by isolating state to an instance-level variable.

### Next Steps
Experiment with other Visual Studio debugging tools, such as:
- Performance Profiler
- IntelliTrace
- Live Unit Testing

