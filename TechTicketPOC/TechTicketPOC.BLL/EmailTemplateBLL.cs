using System;
using TechTicketPOC.BLL.Interfaces;
using TechTicketPOC.Entities;

namespace TechTicketPOC.BLL
{
    public class EmailTemplateBLL
    {
        private readonly IEmailTemplateRepository _repository;

        public EmailTemplateBLL(IEmailTemplateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public EmailTemplateDTO? GetEmailTemplate(int requestId)
        {
            return _repository.GetEmailTemplate(requestId);
        }
    }
}

