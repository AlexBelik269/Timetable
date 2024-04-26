using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace TimetableWPF.Classes
{
    public class Teacher: Availability
    {
        public string _Type { get; set; }
        public List<string> _TeachesSubject { get; set; }

        public Teacher()
        {
        }
    }
}
