using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTicketPOC.DAL.POCOEntities;
using TechTicketPOC.Entities;
using static AutoMapper.Mapper;

namespace TechTicketPOC.DAL
{
    public static class EmailTemplateDAL
    {
        public static EmailTemplateDTO GetEmailTemplate(int requestId)
        {
            using (var dbSession = DocumentStoreHolder.Store.OpenSession())
            {

                try
                {
                    var emailTemplate = dbSession.Query<EmailTemplate>()
                                    .FirstOrDefault(et => et.RequestId == requestId);


                    if (emailTemplate == null)
                        return null;

                    var templateFields = dbSession.Load<EmailTemplateField>(emailTemplate.TemplateFieldIds.Cast<ValueType>())
                                                  .OrderBy(tf => tf.FieldOrder)
                                                  .ToList();

                    var emailTemplateDTO = Map<EmailTemplateDTO>(emailTemplate);

                    if (templateFields.Count > 0)
                    {
                        emailTemplateDTO.Fields = new List<EmailTemplateFieldDTO>();

                        // Collect all field option IDs up front and load them in a single batch
                        // to avoid N+1 queries (one per template field).
                        var allFieldOptionIds = templateFields
                            .Where(tf => tf.FieldOptionsIds != null && tf.FieldOptionsIds.Count > 0)
                            .SelectMany(tf => tf.FieldOptionsIds)
                            .Distinct()
                            .Cast<ValueType>()
                            .ToList();

                        var allFieldOptionsMap = allFieldOptionIds.Count > 0
                            ? dbSession.Load<FieldOption>(allFieldOptionIds)
                                       .Where(fo => fo != null)
                                       .ToDictionary(fo => fo.Id)
                            : new Dictionary<int, FieldOption>();

                        templateFields.ForEach((tf) =>
                        {
                            var fieldDTO = Map<EmailTemplateFieldDTO>(tf);

                            if (tf.FieldOptionsIds != null && tf.FieldOptionsIds.Count > 0)
                            {
                                var fieldOptions = tf.FieldOptionsIds
                                    .Where(id => allFieldOptionsMap.ContainsKey(id))
                                    .Select(id => allFieldOptionsMap[id])
                                    .ToList();

                                fieldDTO.FieldOptions = Map<List<FieldOptionDTO>>(fieldOptions);
                            }

                            emailTemplateDTO.Fields.Add(fieldDTO);
                        });
                    }

                    return emailTemplateDTO;
                }
                catch
                {
                    dbSession.Advanced.Clear();
                    throw;
                }

            }
        }
    }

}
