using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTicketPOC.Entities
{
    public class DivisionDTO : IAuditable
    {
        public int Id { get; set; }
        public string DivisionName { get; set; }
        public List<RequestDTO> Requests { get; set; }
        public string CreatedBy { get ; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
