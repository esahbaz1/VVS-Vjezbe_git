using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
    public interface ITaskAssignmentService
    {
        void AssignTaskToUser(Task task, string user);
        void AssignTaskAutomatically(Task task);
    }


}
