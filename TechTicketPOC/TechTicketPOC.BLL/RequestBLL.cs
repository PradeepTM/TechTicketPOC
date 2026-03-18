using System;
using System.Collections.Generic;
using TechTicketPOC.BLL.Interfaces;
using TechTicketPOC.Entities;

namespace TechTicketPOC.BLL
{
    public class RequestBLL
    {
        private readonly IRequestRepository _repository;

        public RequestBLL(IRequestRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<RequestDTO>? GetRequests(int divisionId)
        {
            var requests = _repository.GetRequests(divisionId);

            if (requests == null) return null;

            return requests;
        }
    }
}

