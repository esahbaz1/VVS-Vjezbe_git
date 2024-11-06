using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace VVSProjekat
{
    public class Reminder
    {
        public Task Task { get; set; }
        public DateTime ReminderTime => Task.Deadline.AddMinutes(-Task.ReminderMinutes);

        public Reminder(Task task)
        {
            Task = task;
        }

        public bool IsDue()
        {
            return !Task.IsCompleted && DateTime.Now >= ReminderTime;
        }
    }
}

