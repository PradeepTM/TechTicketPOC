using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTicketDB
{
    public class ViewModelDTO
    {
        public template template { get; set; }
        public List<field> fields { get; set; }
    }
}
