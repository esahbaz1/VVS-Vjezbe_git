using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVSProjekat
{
    public class Priority
    {
        public string Level { get; set; }

        public Priority(string level)
        {
            Level = level;
        }

        public override string ToString()
        {
            return Level;
        }
    }
}


