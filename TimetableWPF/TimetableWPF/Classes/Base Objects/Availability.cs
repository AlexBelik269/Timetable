using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableWPF.Classes
{
    public class Availability: Named
	{
		public List<string> _Availability { get; set; }

		public Availability(string name, List<string> availability): base(name)
		{
            _Availability = availability;
		}
        public Availability()
        {

        }
    }
}
