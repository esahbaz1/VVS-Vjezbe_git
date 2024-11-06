using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
    public interface ITaskService
    {
        void AddTask(Task task);
        void DisplayTasks();
        void SetPriority(string taskName, string newPriority);
        void GenerateReport();
        void CheckReminders();
        Task GetTaskByName(string taskName);

    }
}
