using System.Collections.Generic;
using TechTicketPOC.Entities;

namespace TechTicketPOC.BLL.Interfaces
{
    public interface IDivisionRepository
    {
        List<DivisionDTO> GetDivisions();
    }
}
