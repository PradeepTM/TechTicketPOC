using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTicketPOC.Entities;

namespace TechTicketPOC.DAL.POCOEntities
{
    public class Division : IAuditable
    {
        public int Id { get; set; }
        public string DivisionName { get; set; }
        public string CreatedBy { get ; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
