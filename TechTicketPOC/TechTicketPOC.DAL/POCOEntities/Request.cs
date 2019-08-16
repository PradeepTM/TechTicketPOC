using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTicketPOC.Entities;

namespace TechTicketPOC.DAL.POCOEntities
{
    public class Request : IAuditable
    {
        public int Id { get; set; }
        public string RequestName { get; set; }
        public int DivisionId { get; set; }
        public int? ParentRequestId { get; set; }
        public List<int> ChildRequestIds { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
