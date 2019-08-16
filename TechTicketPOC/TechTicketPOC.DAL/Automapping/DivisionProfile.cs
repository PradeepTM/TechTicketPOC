using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTicketPOC.DAL.POCOEntities;
using TechTicketPOC.Entities;

namespace TechTicketPOC.DAL.Automapping
{
    public class DivisionProfile : Profile
    {

        public DivisionProfile()
        {
            CreateMap<Division, DivisionDTO>();
        }
    }
}
