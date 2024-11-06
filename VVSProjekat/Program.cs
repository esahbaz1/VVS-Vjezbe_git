using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using VVSProjekat;
using Task = VVSProjekat.Task;

class Program
{
    static void Main(string[] args)
    {
        ITaskService taskService = new TaskService();

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===== Task Management =====");
            Console.ResetColor();

            Console.WriteLine("Choose an option:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Display Tasks");
            Console.WriteLine("3. Set Task Priority");
            Console.WriteLine("4. Generate Report");
            Console.WriteLine("5. Check Reminders");
            Console.WriteLine("6. Mark Task as Completed");
            Console.WriteLine("7. Exit");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter choice: ");
            var choice = Console.ReadLine();
            Console.ResetColor();

            try
            {
                Console.Clear(); // Clear the screen before displaying each option window

                switch (choice)
                {
                    case "1":
                        AddTask(taskService);
                        break;
                    case "2":
                        taskService.DisplayTasks();
                        break;
                    case "3":
                        SetTaskPriority(taskService);
                        break;
                    case "4":
                        taskService.GenerateReport();
                        break;
                    case "5":
                        taskService.CheckReminders();
                        break;
                    case "7":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Thank you for using our application!");
                        Console.ResetColor();
                        return;
                    case "6":
                        MarkTaskAsCompleted(taskService);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Unknown option.");
                        Console.ResetColor();
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nPress Enter to go back to the main menu...");
                Console.ResetColor();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
                Console.ResetColor();
            }
        }
    }

    static void AddTask(ITaskService taskService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Add New Task ===");
        Console.ResetColor();

        Console.Write("Enter task name: ");
        string name = Console.ReadLine();

        Console.Write("Enter deadline (yyyy-MM-dd): ");
        DateTime deadline = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter reminder in minutes: ");
        int reminder = int.Parse(Console.ReadLine());

        Console.Write("Enter priority (low, medium, high): ");
        string priorityLevel = Console.ReadLine();

        Console.Write("Enter category: ");
        string categoryName = Console.ReadLine();

        Console.Write("Is the task completed? (yes/no): ");
        string completedInput = Console.ReadLine();
        bool isCompleted = completedInput.Equals("yes", StringComparison.OrdinalIgnoreCase);

        var task = new Task(
            name,
            deadline,
            reminder,
            new Priority(priorityLevel),
            new Category(categoryName)
        )
        {
            IsCompleted = isCompleted
        };

        taskService.AddTask(task);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Task added successfully!");
        Console.ResetColor();
    }

    static void MarkTaskAsCompleted(ITaskService taskService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Mark Task as Completed ===");
        Console.ResetColor();

        Console.Write("Enter task name to mark as completed: ");
        string taskName = Console.ReadLine();

        var task = taskService.GetTaskByName(taskName);
        if (task != null)
        {
            task.IsCompleted = true;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Task '{taskName}' has been marked as completed.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Task not found.");
        }
        Console.ResetColor();
    }

    static void SetTaskPriority(ITaskService taskService)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Set Task Priority ===");
        Console.ResetColor();

        Console.Write("Enter task name to change priority: ");
        string taskName = Console.ReadLine();
        Console.Write("Enter new priority: ");
        string newPriority = Console.ReadLine();

        taskService.SetPriority(taskName, newPriority);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Priority updated successfully!");
        Console.ResetColor();
    }
}
