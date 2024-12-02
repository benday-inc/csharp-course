# Hands-On Lab: Debugging with Breakpoints in Visual Studio 2022

## Lab Overview
In this lab, you will debug a multithreaded C# application that contains a bug...or multiple bugs. You will explore various types of breakpoints available in Visual Studio 2022 to pinpoint and diagnose the issue systematically. By the end of this lab, you will have hands-on experience using line breakpoints, conditional breakpoints, hit count breakpoints, data breakpoints, function breakpoints, and tracepoints.

### Prerequisites
- Visual Studio 2022 installed

## Step 1: The Sample Code

1. Open **Visual Studio 2022**
2. Open the **DebuggingLab.sln** solution file
3. In **Solution Explorer** you should see a project with three classes that looks similar to the image below

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241126162813626-3168185.png" alt="image-20241126162813626" style="zoom:50%;" />

We're trying to fix some data processing code that someone else wrote and something strange is happening.  The processed data should be organized by user but instead it's coming out as a jumbled mess. And sometimes it throws exceptions, too.



## Step 2: Run the Application & View the Bug

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

We're seeing data from both users when we should only be seeing output from a single user.  **What's going on?**
<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241126163505731-3168185.png" alt="image-20241126163505731" style="zoom:50%;" />

—

## Step 3: Debugging with Breakpoints

### Task 1: Line Breakpoint

This is going to be a pretty basic demo of adding a breakpoint to a line and viewing the contents of variables using the debugger.  Don't worry, we'll get to harder things in a bit.

1. In DataProcessor.cs, set a breakpoint on the following line in the `ProcessData` method by clicking in the left margin:
   ```csharp
   _userData.Data.Add($"{_userId}-{item}");
   ```
   
   
   
   ![image-20241127084354635](/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127084354635-3168185.png)
   
2. Run the program in debug mode (`F5`).
3. When the breakpoint hits, let's inspect the values in the Autos window for:
   - The value of `_userId`
   - The contents of `_userData.Data`
   
   <img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127084657668-3168185.png" alt="image-20241127084657668" style="zoom:50%;" />

   <img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127084721655-3168185.png" alt="image-20241127084721655" style="zoom:50%;" />
   
4. If the Autos window is not visible, you can open it by going to **Debug | Windows | Autos**:

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241126154347555-3168185.png" alt="image-20241126154347555" style="zoom:50%;" />

5. Press **F5** to continue execution. 
6. The breakpoint should get hit a another time...and again...and again...  

#### Run Ignoring Breakpoints
Let's assume that you've seen enough of this breakpoint.  You could clear it &dash; but sometimes when you're debugging, you'll want to use that breakpoint again but just skip ahead to a different part of the code.  This is where **Run To Cursor Ignoring Breakpoints** can be helpful.

7. Open **Program.cs** 
8. Click anywhere on **Line 24**. The goal is to place your cursor on this line
 ```csharp
   Console.WriteLine("Simulation complete.");
 ```

9. Right-click on **Line 24** and select **Run To Cursor Ignoring Breakpoints** from the context menu

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127085651206-3168185.png" alt="image-20241127085651206" style="zoom:50%;" />

10. Adding **Run To Cursor Ignoring Breakpoints** essentially adds a temporary line breakpoint. Execution should be paused and you should now be on **Line 24**

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127085853307-3168185.png" alt="image-20241127085853307" style="zoom:50%;" />

11. Press **F5** or click **Continue** to resume execution

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127090011723-3168185.png" alt="image-20241127090011723" style="zoom:50%;" />



### Task 2: Conditional Breakpoint

At this point, we haven't figured much out regarding our bug.  It's still a mystery and the data is a jumble.  But it's clear that processed data for users is being recorded to the wrong user account.   

**Conditional Breakpoints** can be helpful for stopping when certain data scenarios occur. Think of this as a debugging hypothesis. In our case, it would be nice to know:

* Are we actually writing processed data to the wrong user account?
* If we are doing that, what is happening at that moment?

Below is a class diagram of the sample project.  When **DataProcessor** is processing data for a user, it records the data that's been processed in the **ProcessedData** class.  

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127094245308-3168185.png" alt="image-20241127094245308" style="zoom:50%;" />

What we're going to do now is to modify the breakpoint so that it breaks to the debugger if the ProcessedData.Username doesn't match what we expect.

1. You should still have a breakpoint in **DataProcessor.cs** at **Line 35**.  

 ```csharp
   _userData.Data.Add($"{_userId}-{item}");
 ```

2. Change this breakpoint into a conditional breakpoint on the same line. To do this, **right-click** the breakpoint. From the context menu, choose **Conditions...** 

   <img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127095007545-3168185.png" alt="image-20241127095007545" style="zoom:50%;" />

   

3. You should now see a **Breakpoint Settings** dialog. Type the following text into the **Conditions** textbox and press **Enter**
   
```csharp
_userData.Username != _userId
```

   ![image-20241127095246067](/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127095246067-3168185.png)
4. Press **F5** to Run the program in debug mode.
5. The debugger should break at your conditional breakpoint

The **Autos** window and the **Locals** window for the debugger probably aren't showing you what you need to see all in one place.  Specifically, I'd like to be able to see the current value of `_userId` and of `_userData.Username`.

6. Click the **Watch** tab in the debugger window. 
7. Click where it says **Add item to watch** and add an item for both `_userId` and `userData`
8. Expand the entry for **_userData** so that you can see all the values.  It should look something like the image below.  

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127095940527-3168185.png" alt="image-20241127095940527" style="zoom:50%;" />

Notice how the username values do not match.  That's not right.

9. Click the **Stop** button to stop debugging

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127100325486-3168185.png" alt="image-20241127100325486" style="zoom:50%;" />



### Task 3: Hit Count Breakpoint

When you're debugging code that's running in a loop, sometimes it can be helpful to stop after a certain number of iterations through the loop.  You can do this using a **Hit Count** condition on a breakpoint

1. Remove the conditional breakpoint at **Line 35** in **DataProcessor.cs**
2. Add a new line breakpoint on the same line
3. Right-click the breakpoint > Choose **Conditions...**

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127095007545-3168185.png" alt="image-20241127095007545" style="zoom:50%;" />

4. You should see the **Breakpoint Settings** editor. From the drop-down box, choose **Hit Count** then set the value to **5**. Then press **Enter**

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127100957972-3168185.png" alt="image-20241127100957972" style="zoom:50%;" />

4. Run the program in debug mode
5. You should hit the breakpoint and the username values probably don't match

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127101136853-3168185.png" alt="image-20241127101136853" style="zoom:50%;" />

Yup.  It's still broken.  Let's keep digging.

### Task 4: Delete All Breakpoints

Right now we only have one breakpoint but you can easily get into breakpoint sprawl. Helpfully enough, there's an option to delete all breakpoints. 

1. From the **Main Menu** for Visual Studio, choose the **Debug** menu then choose **Delete All Breakpoints** 

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127101526088-3168185.png" alt="image-20241127101526088" style="zoom:50%;" />



### Task 5: Tracepoint

Sometimes you want to write a value to the console during execution rather than actually stoping execution and looking at the values in the debugger.  You could do the time-honored practice of adding `Console.WriteLine()` or `Debug.WriteLine()` calls to your code.  That works and it's easy to understand but -- well -- but that also leaves you with potentially lots of extra calls in your code.

If you need a way to have the equivalent of a temporary `Console.WriteLine()`, you can use a **Tracepoint**.

1. Let's add a Tracepoint in **DataProcessor.cs** at **Line 35**. Go to that line and **right-click** in the left gutter. Choose **Insert Tracepoint** from the context menu

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127103834504-3168185.png" alt="image-20241127103834504" style="zoom:50%;" />

2. You should see the **Breakpoint Settings** dialog
3. Under **Actions** enter the following in the **Show a message in the Output Window** textbox

```
TRACEPOINT! -- Adding processed item {item} to user data for user {_userId}
```



<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127103951798-3168185.png" alt="image-20241127103951798" style="zoom:50%;" />

4. Press **Enter**
5. Let's make sure that the **Output** window is visible.  From the Visual Studio **Main Menu** choose **Debug > Windows > Output**
6. Press **F5** to run in debug mode
7. The application should run and complete.
8. Go to the **Output** window

You should see a bunch of tracepoint messages in the output.

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127104320504-3168185.png" alt="image-20241127104320504" style="zoom:50%;" />


### Task 6 (Optional): Fix the Bug
I'm not sure if you've figured it out yet but this code is not great and whoever wrote it probably didn't understand `static` variables very well. If a variable is `static`, that means that there is a single instance that is shared and (potentially) accessible by all instances of the class.  In this case, there was a combination of a race condition (a type of multithreading mistake) to initialize the _userData variable and then to make it worse, that _userData variable was marked as static. 

All the changes are in **DataProcessor.cs**.

1. Change the `_userData` member variable to

```csharp
private ProcessedData _userData;
```

2. Change the code for the constructor to

```csharp
public DataProcessor(string userId)
{
    _userId = userId;
    _userData = new();
    EnsureDataCacheIsSaved(_userId, _userData);
}
```

3. Change the ProcessData() method to

```csharp
public void ProcessData(string[] data)
{
    foreach (var item in data)
    {
        Thread.Sleep(new Random().Next(100, 500));

        _userData.Data.Add($"{_userId}-{item}");
    }
}
```

4. When you've completed the changes described above, the code for **DataProcessor** should look like the following code:

```csharp
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace DebuggingLab;

class DataProcessor
{
    private readonly string _userId;
    private static readonly ConcurrentDictionary<string, ProcessedData> _cache = new();
    private ProcessedData _userData;
    private static readonly object _lock = new();

    public DataProcessor(string userId)
    {
        _userId = userId;
        _userData = new();
        EnsureDataCacheIsSaved(_userId, _userData);
    }

    public void ProcessData(string[] data)
    {
        foreach (var item in data)
        {
            Thread.Sleep(new Random().Next(100, 500));

            _userData.Data.Add($"{_userId}-{item}");
        }
    }

    private static void EnsureDataCacheIsSaved(string userId, ProcessedData data)
    {
        if (_cache.ContainsKey(userId) == false)
        {
            _cache.TryAdd(userId, data);
        }
    }

    public string[] GetProcessedData()
    {
        lock (_lock)
        {
            if (_cache.TryGetValue(_userId, out var processedData))
            {
                return processedData.Data.ToArray();
            }
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Warning: No data found for {_userId}. Returning empty array.");
            return Array.Empty<string>();
        }
    }
}
```
5. Press **F5** to run the application
5. You should see output similar to the screenshot below and all the user data should match the appropriate username.

<img src="/Users/benday/code/benday-inc/csharp-course/lab-01-debugging/image-20241127111324873-3168185.png" alt="image-20241127111324873" style="zoom:50%;" />

Fixed it!





