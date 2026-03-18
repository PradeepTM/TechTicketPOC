using System;
using System.Collections.Generic;
using TechTicketPOC.BLL.Interfaces;
using TechTicketPOC.Entities;

namespace TechTicketPOC.BLL
{
    public class DivisionBLL
    {
        private readonly IDivisionRepository _repository;

        public DivisionBLL(IDivisionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<DivisionDTO>? GetDivisions()
        {
            var divisions = _repository.GetDivisions();

            if (divisions == null) return null;

            return divisions;
        }
    }
}

