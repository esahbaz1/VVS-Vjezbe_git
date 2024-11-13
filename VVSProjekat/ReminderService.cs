using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace VVSProjekat
{
    public class ReminderService : IReminderService
    {
        private Timer _timer;
        private List<Reminder> _reminders = new List<Reminder>();

        public ReminderService()
        {
            _timer = new Timer(CheckReminders, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        public void SetReminder(Reminder reminder)
        {
            reminder.ReminderTime = SetOptimizedReminderTime(reminder.Task);
            _reminders.Add(reminder);

            // Sortiraj zadatke prema prioritetu i kategoriji svaki put kada dodamo novi
            _reminders = _reminders.OrderByDescending(r => r.Task.TaskPriority)
                                   .ThenBy(r => r.Task.Category)
                                   .ToList();
        }

        private DateTime SetOptimizedReminderTime(Task task)
        {
            int priorityFactor = task.TaskPriority switch
            {
                Priority.High => -3,
                Priority.Medium => -1,
                Priority.Low => 0,
                _ => 0
            };

            DateTime optimalReminderTime = task.DueDate.AddHours(priorityFactor);
            if (optimalReminderTime < DateTime.Now)
            {
                optimalReminderTime = DateTime.Now.AddMinutes(10);
            }

            Console.WriteLine($"Podsjetnik za '{task.Title}' postavljen je za {optimalReminderTime:dd/MM/yyyy HH:mm}");
            return optimalReminderTime;
        }

        private void CheckReminders(object state)
        {
            foreach (var reminder in _reminders.ToList())
            {
                if (DateTime.Now >= reminder.ReminderTime)
                {
                    Notify(reminder);
                    _reminders.Remove(reminder);
                }
            }
        }

        public void Notify(Reminder reminder)
        {
            Console.WriteLine($"Reminder: Time to work on task '{reminder.Task.Title}'!");
        }
    }
}