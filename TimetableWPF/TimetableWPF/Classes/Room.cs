using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimetableWPF.Classes
{
    public class Room: Named
    {
        public List<string> Type { get; set; }

        public Room(string name): base(name) { }

    }
}
