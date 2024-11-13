using System;
using System.Collections.Generic;
using System.Linq;

namespace VVSProjekat
{
    public class ReportService : IReportService
    {
        public List<Task> FilterTasksByDateRange(List<Task> tasks, DateTime startDate, DateTime endDate)
        {
            return tasks.Where(t => t.DueDate >= startDate && t.DueDate <= endDate).ToList();
        }

        public List<Task> SortTasksByPriorityAndCategory(List<Task> tasks)
        {
            return tasks
                .OrderByDescending(t => t.TaskPriority)
                .ThenBy(t => t.Category.Name)
                .ToList();
        }

        public Report GenerateReport(DateTime startDate, DateTime endDate, List<Task> tasks)
        {
            var filteredTasks = FilterTasksByDateRange(tasks, startDate, endDate);
            var sortedTasks = SortTasksByPriorityAndCategory(filteredTasks);

            int completedTasks = sortedTasks.Count(t => t.IsCompleted);
            int pendingTasks = sortedTasks.Count(t => !t.IsCompleted);

            return new Report(startDate, endDate, sortedTasks)
            {
                TotalTasks = sortedTasks.Count,
                CompletedTasks = completedTasks,
                PendingTasks = pendingTasks,
                HighPriorityTasks = sortedTasks.Count(t => t.TaskPriority == Priority.High),
                MediumPriorityTasks = sortedTasks.Count(t => t.TaskPriority == Priority.Medium),
                LowPriorityTasks = sortedTasks.Count(t => t.TaskPriority == Priority.Low)
            };
        }

        public void DisplayTasksGroupedByPriorityAndCategory(Report report)
        {
            var groupedByPriority = report.Tasks
                .GroupBy(t => t.TaskPriority)
                .OrderByDescending(g => g.Key);

            foreach (var priorityGroup in groupedByPriority)
            {
                Console.WriteLine($"\nPrioritet: {priorityGroup.Key}");

                var groupedByCategory = priorityGroup
                    .GroupBy(t => t.Category.Name)
                    .OrderBy(g => g.Key);

                foreach (var categoryGroup in groupedByCategory)
                {
                    Console.WriteLine($"\tKategorija: {categoryGroup.Key}");
                    foreach (var task in categoryGroup)
                    {
                        Console.WriteLine($"\t\t- {task.Title}, Rok: {task.DueDate:dd/MM/yyyy}, Završeno: {task.IsCompleted}");
                    }
                }
            }
        }
    }
}
