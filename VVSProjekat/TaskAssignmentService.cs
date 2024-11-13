using VVSProjekat;
using System;

namespace VVSProjekat
{

    public class TaskAssignmentService : ITaskAssignmentService
    {
        public void AssignTaskToUser(Task task, string user)
        {
            Console.WriteLine($"Task '{task.Title}' assigned to {user}");
        }

        public void AssignTaskAutomatically(Task task)
        {
            // Jednostavna logika za automatsku dodelu na osnovu prioritetnog zadatka
            if (task.TaskPriority == Priority.High)
            {
                Console.WriteLine($"Task '{task.Title}' assigned automatically to a high-priority user.");
            }
            else
            {
                Console.WriteLine($"Task '{task.Title}' assigned automatically based on criteria.");
            }
        }
    }


}


