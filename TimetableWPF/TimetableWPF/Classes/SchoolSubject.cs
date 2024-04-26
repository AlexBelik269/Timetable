using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableWPF.Classes
{
	public class SchoolSubject: Named
	{
		public string _Type { get; set; }

		public SchoolSubject(string name, string category): base(name) 
		{
            _Type = category;
		}
        public SchoolSubject()
        {
        }
    }
}
