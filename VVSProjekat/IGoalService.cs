using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
    public interface IGoalService
    {
        void SetMonthlyGoal(int numberOfTasksPerMonth);
        void TrackProgress();
    }


}
