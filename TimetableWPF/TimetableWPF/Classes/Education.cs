using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableWPF.Classes
{
    public class Education: Named
    {
        public string _Type { get; set; }
        public List<string> _Subjects { get; set; }
        public Education() { }

    }
}
