using System;
using System.Collections.Generic;
using System.Linq;

namespace VVSProjekat
{
    public class GoalService : IGoalService
    {
        private int _dailyGoal;
        private int _weeklyGoal;
        private int _monthlyGoal;
        private int _tasksCompletedToday;
        private int _tasksCompletedThisWeek;
        private int _tasksCompletedThisMonth;
        private DateTime _lastTrackedDate;
        private DateTime _weekStartDate;
        private DateTime _monthStartDate;
        private List<DateTime> _completedTasksHistory;

        public GoalService()
        {
            _completedTasksHistory = new List<DateTime>();
            _weekStartDate = DateTime.Today;
            _monthStartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        }

        public void SetDailyGoal(int numberOfTasksPerDay)
        {
            _dailyGoal = numberOfTasksPerDay;
            _tasksCompletedToday = 0;
            _lastTrackedDate = DateTime.Today;
            Console.WriteLine($"Daily goal set: {_dailyGoal} tasks per day.");
        }

        public void SetWeeklyGoal(int numberOfTasksPerWeek)
        {
            _weeklyGoal = numberOfTasksPerWeek;
            _tasksCompletedThisWeek = 0;
            _weekStartDate = DateTime.Today;
            Console.WriteLine($"Weekly goal set: {_weeklyGoal} tasks per week.");
        }

        public void SetMonthlyGoal(int numberOfTasksPerMonth)
        {
            _monthlyGoal = numberOfTasksPerMonth;
            _tasksCompletedThisMonth = 0;
            _monthStartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            Console.WriteLine($"Monthly goal set: {_monthlyGoal} tasks per month.");
        }

        public void TrackProgress()
        {
            ResetProgressIfNeeded();

            int remainingDailyTasks = _dailyGoal - _tasksCompletedToday;
            int remainingWeeklyTasks = _weeklyGoal - _tasksCompletedThisWeek;
            int remainingMonthlyTasks = _monthlyGoal - _tasksCompletedThisMonth;

            Console.WriteLine($"Daily Progress: {_tasksCompletedToday}/{_dailyGoal} tasks completed.");
            Console.WriteLine($"Weekly Progress: {_tasksCompletedThisWeek}/{_weeklyGoal} tasks completed.");
            Console.WriteLine($"Monthly Progress: {_tasksCompletedThisMonth}/{_monthlyGoal} tasks completed.");

            if (remainingDailyTasks > 0)
                Console.WriteLine($"You need to complete {remainingDailyTasks} more tasks today to meet your daily goal.");

            if (remainingWeeklyTasks > 0)
                Console.WriteLine($"You need to complete {remainingWeeklyTasks} more tasks this week to meet your weekly goal.");

            if (remainingMonthlyTasks > 0)
                Console.WriteLine($"You need to complete {remainingMonthlyTasks} more tasks this month to meet your monthly goal.");
        }

        public void CompleteTask()
        {
            ResetProgressIfNeeded();

            _tasksCompletedToday++;
            _tasksCompletedThisWeek++;
            _tasksCompletedThisMonth++;
            _completedTasksHistory.Add(DateTime.Now);

            Console.WriteLine($"Task completed! You have completed {_tasksCompletedToday} out of {_dailyGoal} tasks today.");

            if (_tasksCompletedToday >= _dailyGoal)
            {
                Console.WriteLine("Congratulations! You have met your daily goal!");
            }

            if (_tasksCompletedThisWeek >= _weeklyGoal)
            {
                Console.WriteLine("Great job! You have met your weekly goal!");
            }

            if (_tasksCompletedThisMonth >= _monthlyGoal)
            {
                Console.WriteLine("Excellent! You have met your monthly goal!");
            }
        }

        public void ShowTaskCompletionHistory()
        {
            Console.WriteLine("Task Completion History:");
            foreach (var date in _completedTasksHistory)
            {
                Console.WriteLine($"- Task completed on {date.ToString("yyyy-MM-dd HH:mm:ss")}");
            }
        }

        public void ShowSuccessRate()
        {
            int totalDays = (DateTime.Today - _monthStartDate).Days + 1;
            int daysWithTasks = _completedTasksHistory.Select(d => d.Date).Distinct().Count();
            double successRate = (double)daysWithTasks / totalDays * 100;

            Console.WriteLine($"Your success rate for this month is {successRate:F2}% based on daily completions.");
        }

        private void ResetProgressIfNeeded()
        {
            // If it's a new day, reset daily progress
            if (_lastTrackedDate < DateTime.Today)
            {
                _tasksCompletedToday = 0;
                _lastTrackedDate = DateTime.Today;
                Console.WriteLine("New day! Daily progress has been reset.");
            }

            // If it's a new week, reset weekly progress
            if (_weekStartDate <= DateTime.Today.AddDays(-7))
            {
                _tasksCompletedThisWeek = 0;
                _weekStartDate = DateTime.Today;
                Console.WriteLine("New week! Weekly progress has been reset.");
            }

            // If it's a new month, reset monthly progress
            if (_monthStartDate.Month < DateTime.Today.Month || _monthStartDate.Year < DateTime.Today.Year)
            {
                _tasksCompletedThisMonth = 0;
                _monthStartDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                Console.WriteLine("New month! Monthly progress has been reset.");
            }
        }
    }
}