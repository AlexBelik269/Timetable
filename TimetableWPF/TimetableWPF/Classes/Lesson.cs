using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableWPF.Classes
{
    public class Lesson: Named
    {
        public string _LessonDay { get; set; }
        public string _LessonStartTime { get; set; }
        public string _LessonEndTime { get; set; }
        public string _SchoolSubject { get; set; }
        public string _Lehrer { get; set; }
        public string _Class { get; set; }
        public string _Room { get; set; }

        public Lesson()
        { 

        }

    }
}
