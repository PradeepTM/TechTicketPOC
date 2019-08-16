using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTicketPOC.DAL;
using TechTicketPOC.Entities;
using static AutoMapper.Mapper;

namespace TechTicketPOC.BLL
{
    public class RequestBLL
    {
        public List<RequestDTO> GetRequests(int divisionId)
        {
            var requests = RequestDAL.GetRequests(divisionId);

            if (requests == null) return null;

            return Map<List<RequestDTO>>(requests);
        }
    }
}
