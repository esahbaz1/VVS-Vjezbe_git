using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace VVSProjekat
{
    public class Report
    {
        public List<Task> Tasks { get; set; }

        public Report(List<Task> tasks)
        {
            Tasks = tasks;
        }

        public void Generate()
        {
            int totalTasks = Tasks.Count;
            int completedTasks = Tasks.Count(t => t.IsCompleted);

            Console.WriteLine("Report:");
            Console.WriteLine($"Total tasks: {totalTasks}");
            Console.WriteLine($"Completed tasks: {completedTasks}");
            Console.WriteLine($"Incomplete tasks: {totalTasks - completedTasks}");
        }
    }
}

