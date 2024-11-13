using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
    public class TaskService : ITaskService
    {
        private List<Task> _tasks = new List<Task>();
        private TaskAssignmentService taskAssignmentService;
        public List<Task> SortTasksByPriority1()
        {
            var tasksArray = _tasks.ToArray();  //Heapsort algoritmom
            int n = tasksArray.Length;

            // Izgradnja heap strukture
            for (int i = n / 2 - 1; i >= 0; i--)
               Sortiranje(tasksArray, n, i);

            // Ekstrakcija elemenata iz heap-a
            for (int i = n - 1; i > 0; i--)
            {
                // Premještanje trenutnog korena na kraj
                (tasksArray[0], tasksArray[i]) = (tasksArray[i], tasksArray[0]);

                //Sortiranje smanjeni heap
               Sortiranje(tasksArray, i, 0);
            }

            // Vraćamo sortiranu listu
            return tasksArray.ToList();
        }

        private void Sortiranje(Task[] tasks, int n, int i)
        {
            int largest = i; // Inicijalizacija najvećeg kao korijena
            int left = 2 * i + 1; // Lijevi
            int right = 2 * i + 2; // Desni

            // Ako je lijevi veći od korijena
            if (left < n && tasks[left].TaskPriority > tasks[largest].TaskPriority)
                largest = left;

            // Ako je desni veći od najvećeg do sada
            if (right < n && tasks[right].TaskPriority > tasks[largest].TaskPriority)
                largest = right;

            // Ako najveći nije korijen
            if (largest != i)
            {
                (tasks[i], tasks[largest]) = (tasks[largest], tasks[i]);

                // RekurzivnoSortiranje pod-stablo
               Sortiranje(tasks, n, largest);
            }
        }

        public void CreateTask(Task task)
        {
            _tasks.Add(task);
            Console.WriteLine($"Task '{task.Title}' created!");
        }

       

        public void UpdateTask(Task task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Title == task.Title);
            if (existingTask != null)
            {
                existingTask.TaskPriority = task.TaskPriority;
                existingTask.IsCompleted = task.IsCompleted;
                Console.WriteLine($"Task '{task.Title}' updated!");
            }
        }

        public List<Task> GetTasks() => _tasks;

        public List<Task> SortTasksByPriority() => _tasks.OrderBy(t => t.TaskPriority).ToList();

        public void ArchiveCompletedTasks()
        {
            _tasks.RemoveAll(t => t.IsCompleted);
            Console.WriteLine("Completed tasks archived!");
        }

        public void GenerateStatistics(DateTime startDate, DateTime endDate)
        {
            var tasksInRange = _tasks.Where(t => t.DueDate >= startDate && t.DueDate <= endDate).ToList();
            Console.WriteLine($"Statistics from {startDate} to {endDate}:");
            Console.WriteLine($"Total tasks: {tasksInRange.Count}");
        }
        public List<Task> PrioritizeTasks()
        {
            return _tasks.OrderByDescending(t => t.TaskPriority)
                         .ThenBy(t => t.DueDate)
                         .ToList();
        }
        public void AssignTaskByPriorityAndCategory()
        {
            Console.Write("Unesite prioritet (Low, Medium, High): ");
            string priorityInput = Console.ReadLine();
            Priority taskPriority;

            // Pokušavamo parsirati prioritet
            if (!Enum.TryParse(priorityInput, true, out taskPriority))
            {
                Console.WriteLine("Neispravan prioritet.");
                return;
            }

            Console.Write("Unesite kategoriju (Posao, Personalno, Generalno): ");
            string categoryInput = Console.ReadLine();

            // Pretraga zadatka sa odgovarajućim prioritetom i kategorijom
            var tasksForAssignment = _tasks
                .Where(t => t.TaskPriority == taskPriority && t.Category.Name.Equals(categoryInput, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (tasksForAssignment.Count > 0)
            {
                // Automatska dodjela zadatka
                var taskForAssignment = tasksForAssignment.First();  // Ili bilo koji zadatak iz liste
                taskAssignmentService.AssignTaskAutomatically(taskForAssignment);
                Console.WriteLine($"Zadatak '{taskForAssignment.Title}' je dodeljen.");
            }
            else
            {
                Console.WriteLine("Nema zadatka sa zadatim prioritetom i kategorijom.");
            }
        }
        }

}
