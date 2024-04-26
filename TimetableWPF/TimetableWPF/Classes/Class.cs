using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableWPF.Classes
{
    public class Class: Availability
    {
        public List<Student> _Students { get; set; }
        public int _EducationYear { get; set; }
        public bool _IsMandatory { get; set; }
        public string _Education { get; set; }
        public Class(string name, List<string> availability, List<Student> students, string education): base(name, availability) 
        {
            _Students = students;
            _Education = education;
        }
        public Class()
        {

        }
    }
}
