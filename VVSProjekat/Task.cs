using System;
using System.Collections.Generic;

namespace VVSProjekat
{
    public class Task
    {    
        public List<Task> SubTasks { get; set; } = new List<Task>();
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority TaskPriority { get; set; }
        public bool IsCompleted { get; set; }
        public Category Category { get; set; }
        public DateTime? AssignedDate { get; set; }

        public Task(string title, string description, DateTime dueDate, Priority priority, Category category)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            TaskPriority = priority;
            Category = category;
        }

        // Dodavanje podzadataka
        public void AddSubTask(Task subTask)
        {
            SubTasks.Add(subTask);
        }
        public DateTime? ReminderTime { get; set; }
    }

    
}
