using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
   

    public class TaskService : ITaskService
    {
        private List<Task> tasks = new List<Task>();

        public void AddTask(Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task), "Task cannot be null");

            tasks.Add(task);
            Console.WriteLine("Task added!");
        }

        public void DisplayTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            var sortedTasks = tasks.OrderBy(t => t.Deadline).ToList();
            Console.WriteLine("Tasks:");
            foreach (var task in sortedTasks)
            {
                Console.WriteLine($"{task.Name} - Due: {task.Deadline.ToShortDateString()}, Priority: {task.Priority}, Category: {task.Category}, Completed: {task.IsCompleted}");
            }
        }


        public void SetPriority(string taskName, string newPriority)
        {
            var task = tasks.FirstOrDefault(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
            if (task != null)
            {
                task.Priority = new Priority(newPriority);
                Console.WriteLine("Priority updated.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }



        public void GenerateReport()
        {
            var report = new Report(tasks);
            report.Generate();
        }


        public void CheckReminders()
        {
            foreach (var task in tasks)
            {
                var reminder = new Reminder(task);
                if (reminder.IsDue())
                {
                    Console.WriteLine($"Reminder: Task '{task.Name}' is due soon!");
                }
            }
        }
        public Task GetTaskByName(string taskName)
        {
            return tasks.FirstOrDefault(t => t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
        }

    }
}
