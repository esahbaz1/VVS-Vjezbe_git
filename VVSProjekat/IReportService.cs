using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
    public interface IReportService
    {
        Report GenerateReport(DateTime startDate, DateTime endDate, List<Task> tasks);
        void DisplayTasksGroupedByPriorityAndCategory(Report report);
    }


}
