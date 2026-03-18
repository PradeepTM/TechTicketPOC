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
        private readonly EmailTemplateBuilder _templateBuilder = new EmailTemplateBuilder();

        public EmailTemplateDTO GetEmailTemplate(int requestId)
        {
            return EmailTemplateDAL.GetEmailTemplate(requestId);
        }

        /// <summary>
        /// Builds the complete email preview HTML (headers + body) by substituting
        /// {{FieldName}} placeholders in the stored template body with the supplied
        /// field values.
        /// </summary>
        public string BuildEmailPreviewHtml(EmailTemplateDTO template,
                                            string subject,
                                            Dictionary<string, string> fieldValues)
        {
            return _templateBuilder.BuildEmailPreviewHtml(template, subject, fieldValues);
        }
    }
}
