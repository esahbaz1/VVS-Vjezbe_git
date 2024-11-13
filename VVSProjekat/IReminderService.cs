using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
    public interface IReminderService
    {
        void SetReminder(Reminder reminder);
        void Notify(Reminder reminder);
    }


}
