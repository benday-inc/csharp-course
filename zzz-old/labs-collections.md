Here’s the list of labs formatted in **Markdown**:

---

# **C# Hands-On Labs: Generic Collections**

## **Lab 1: Working with Lists**
**Objective**: Familiarize students with `List<T>` and basic operations.

1. **Create a `List<int>`** and perform the following operations:
   - Add five integers to the list.
   - Remove one of the integers.
   - Find the largest number in the list.

2. **Extension**: Create a `List<string>` for storing names. Sort the names alphabetically using `List.Sort()` and display them.

---

## **Lab 2: Using Dictionaries**
**Objective**: Understand how to work with key-value pairs using `Dictionary<TKey, TValue>`.

1. **Create a dictionary** to store student names as keys and their grades as values.
   - Add five students and their corresponding grades.
   - Retrieve and print a specific student's grade.
   - Update a student’s grade and print the updated list.

2. **Extension**: Use `TryGetValue` to safely retrieve a student’s grade, and print a message if the student is not found.

---

## **Lab 3: Sets and Duplicates**
**Objective**: Use `HashSet<T>` to handle unique elements and eliminate duplicates.

1. **Create a `HashSet<int>`** with several integers, including duplicates.
   - Print the unique elements of the set.
   - Add new elements to the set and demonstrate that duplicates are ignored.

2. **Challenge**: Use a `HashSet<string>` to store words and show how to find the intersection of two sets.

---

## **Lab 4: Queues and Stacks for Processing**
**Objective**: Use `Queue<T>` and `Stack<T>` for first-in, first-out (FIFO) and last-in, first-out (LIFO) operations.

1. **Queue Example**:
   - Create a `Queue<string>` to represent a customer service line.
   - Add five customers to the queue.
   - Process each customer by dequeuing and printing their name.

2. **Stack Example**:
   - Create a `Stack<int>` to simulate a pile of plates.
   - Push five numbers onto the stack.
   - Pop and print each number, demonstrating LIFO behavior.

---

## **Lab 5: Using LINQ with Generic Collections**
**Objective**: Introduce LINQ queries with generic collections for filtering and sorting data.

1. **Create a `List<string>`** with several city names.
   - Use LINQ to filter cities that start with the letter ‘A’.
   - Sort the cities alphabetically using `OrderBy()`.

2. **Extension**: Create a `List<int>` and use LINQ to find all even numbers.

---

## **Lab 6: Custom Comparers with Sorting**
**Objective**: Implement custom sorting using `IComparer<T>` with `List<T>.Sort()`.

1. **Create a `List<string>`** of book titles.
   - Write a custom comparer to sort the books by **length of title**.
   - Print the list before and after sorting.

2. **Challenge**: Extend the lab to include sorting books by **alphabetical order** if two titles have the same length.

---

## **Lab 7: Generic Methods**
**Objective**: Introduce students to writing and using generic methods.

1. **Write a generic method** that takes a `List<T>` and prints all its elements.
   - Test the method with both `List<int>` and `List<string>`.

2. **Extension**: Write a generic method to find the maximum element in a `List<T>` where `T` implements `IComparable`.

---

## **Lab 8: Custom Generic Classes**
**Objective**: Create and use a custom generic class.

1. **Create a generic class** called `Box<T>` that stores a single value.
   - Add methods to get and set the value.
   - Demonstrate how the class can store both `int` and `string` types.

2. **Challenge**: Extend the `Box<T>` class to include a method for checking if two boxes contain the same value.

---

## **Lab 9: Exploring Concurrent Collections**  
**Objective**: Introduce thread-safe collections using `ConcurrentDictionary<TKey, TValue>`.

1. **Create a `ConcurrentDictionary<string, int>`** to store item stock levels.
   - Add items and demonstrate updating stock safely from multiple threads.

2. **Challenge**: Add logic to remove an item only if its stock is zero.

---

## **Lab 10: Performance Comparisons with Collections**  
**Objective**: Understand the performance differences between different collections.

1. **Create and time** operations (like adding and searching) for:
   - `List<T>`
   - `Dictionary<TKey, TValue>`
   - `HashSet<T>`

2. **Extension**: Use `Stopwatch` to measure how quickly each collection adds and searches for elements. Analyze which collection performs better for different use cases.

---

## **Conclusion**
These labs provide students with practical, hands-on experience with various **generic collections in C#**. They cover fundamental concepts, advanced use cases, and practical applications of **Lists, Dictionaries, Sets, Queues, Stacks, and LINQ**. For additional depth, include tasks involving **error handling** and **thread-safety**, or ask students to build **real-world applications** using these collections.

---

Let me know if you'd like any further customization or additional labs!