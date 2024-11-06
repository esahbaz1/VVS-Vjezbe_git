using System;

namespace VVSProjekat
{
    public class Task
    {
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public int ReminderMinutes { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }
        public Category Category { get; set; }

        public Task(string name, DateTime deadline, int reminderMinutes, Priority priority, Category category)
        {
            Name = name;
            Deadline = deadline;
            ReminderMinutes = reminderMinutes;
            Priority = priority;
            IsCompleted = false;
            Category = category;
        }
    }
}
