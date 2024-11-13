using System;
using System.Collections.Generic;
using System.Linq;

namespace VVSProjekat
{
    public class Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Task> Tasks { get; set; }

        // Dodane statističke varijable
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public int HighPriorityTasks { get; set; }
        public int MediumPriorityTasks { get; set; }
        public int LowPriorityTasks { get; set; }

        public Report(DateTime startDate, DateTime endDate, List<Task> tasks)
        {
            StartDate = startDate;
            EndDate = endDate;
            Tasks = tasks;

            // Inicijalizacija statistike
            CalculateStatistics();
        }

        // Funkcija za računanje statistike
        private void CalculateStatistics()
        {
            TotalTasks = Tasks.Count;
            CompletedTasks = Tasks.Count(t => t.IsCompleted);
            PendingTasks = TotalTasks - CompletedTasks;
            HighPriorityTasks = Tasks.Count(t => t.TaskPriority == Priority.High);
            MediumPriorityTasks = Tasks.Count(t => t.TaskPriority == Priority.Medium);
            LowPriorityTasks = Tasks.Count(t => t.TaskPriority == Priority.Low);
        }

        // Funkcija za generisanje izvještaja s dodanom statistikom
        public void GenerateReport()
        {
            Console.WriteLine($"Report from {StartDate:dd/MM/yyyy} to {EndDate:dd/MM/yyyy}");
            Console.WriteLine($"Total Tasks: {TotalTasks}");
            Console.WriteLine($"Completed Tasks: {CompletedTasks}");
            Console.WriteLine($"Pending Tasks: {PendingTasks}");
            Console.WriteLine($"High Priority Tasks: {HighPriorityTasks}");
            Console.WriteLine($"Medium Priority Tasks: {MediumPriorityTasks}");
            Console.WriteLine($"Low Priority Tasks: {LowPriorityTasks}");
            Console.WriteLine("\nTask Details:");
            foreach (var task in Tasks)
            {
                Console.WriteLine($"Task: {task.Title}, Due: {task.DueDate:dd/MM/yyyy}, Priority: {task.TaskPriority}, Completed: {task.IsCompleted}");
            }
        }
    }
}
