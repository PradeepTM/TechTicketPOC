using System.Collections.Generic;
using TechTicketPOC.Entities;

namespace TechTicketPOC.BLL.Interfaces
{
    public interface IRequestRepository
    {
        List<RequestDTO> GetRequests(int divisionId);
    }
}
