using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
    public interface ITaskService
    {
        void CreateTask(Task task);
        void UpdateTask(Task task);
        List<Task> GetTasks();
        List<Task> SortTasksByPriority();
        void ArchiveCompletedTasks();
        void GenerateStatistics(DateTime startDate, DateTime endDate);
    }

}
