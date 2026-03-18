using TechTicketPOC.Entities;

namespace TechTicketPOC.BLL.Interfaces
{
    public interface IEmailTemplateRepository
    {
        EmailTemplateDTO? GetEmailTemplate(int requestId);
    }
}
