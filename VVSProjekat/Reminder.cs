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
        public DateTime ReminderTime { get; set; }

        public Reminder(Task task, DateTime reminderTime)
        {
            Task = task;
            ReminderTime = reminderTime;
        }

        public void SetReminder()
        {
            Console.WriteLine($"Reminder set for task '{Task.Title}' at {ReminderTime}");
        }
    }

}

