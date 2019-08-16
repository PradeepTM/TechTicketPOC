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
    public class EmailTemplateBLL
    {
        public EmailTemplateDTO GetEmailTemplate(int requestId)
        {
            return EmailTemplateDAL.GetEmailTemplate(requestId);
        }
    }
}
