using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTicketPOC.DAL;
using TechTicketPOC.DAL.POCOEntities;

namespace TechTicketPOC.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureAutomapper();
            //AddDivisions();
            //AddRequests();
            //DeleteAllRequests();
            //var divisions = DivisionDAL.GetDivisions();
            //var requests = RequestDAL.GetRequests(1);
            //AddEmailTemplates(37, "Raiser-Collision", "Raiser-Collision email template", "samik.dam@jamesriverins.com");
            //AddEmailTemplates(34, "Core-Spit", "Core-Spit email template", "Jeffery.Stoner@jamesriverins.com");
            //AddEmailTemplates(38, "Raiser-PIP", "Raiset-PIP email template", "Jason.Couch@jamesriverins.com");
            //var restult = EmailTemplateDAL.GetEmailTemplate(33);
            AddCheckBoxField(1);
            Console.ReadLine();
        }

        private static void AddCheckBoxField(int emailTemplateId)
        {
            var checkBoxField = new EmailTemplateField
            {
                FieldName = "IsClaimsApproved",
                DisplayName = "Is claims approved",
                DataType = "Bool",
                FieldOrder = 1,
                FieldType = "CheckBox",
                IsAllowBlank = false,
                EmailTemplateId = emailTemplateId
            };

            using (var dbSession = DocumentStoreHolder.Store.OpenSession())
            {
                var emailTemplate = dbSession.Load<EmailTemplate>(emailTemplateId);
                dbSession.Store(checkBoxField);
                emailTemplate.TemplateFieldIds.Add(checkBoxField.Id);
                dbSession.SaveChanges();
            }
        }

        private static void ConfigureAutomapper()
        {
            Mapper.Initialize((config) => config.AddProfiles("TechTicketPOC.Test", "TechTicketPOC.DAL"));
        }

        private static void AddRequests()
        {
            using (var dbSession = DocumentStoreHolder.Store.OpenSession())
            {
                var requests = new List<Request>()
                {
                    new Request { RequestName = "Reset", DivisionId = 1 },
                    new Request { RequestName = "Spit", DivisionId = 1 },
                    new Request { RequestName = "Requests", DivisionId = 1 },
                    new Request { RequestName = "Letters", DivisionId = 1 },
                    new Request { RequestName = "Collision", DivisionId = 2 },
                    new Request { RequestName = "PIP", DivisionId = 2 },
                    new Request { RequestName = "Claims", DivisionId = 2 },
                    new Request { RequestName = "Fax", DivisionId = 2 },
                    new Request { RequestName = "Letters", DivisionId = 2 }
                };

                requests.ForEach(r => dbSession.Store(r));
                dbSession.SaveChanges();
            }
        }

        private static void DeleteAllRequests()
        {
            using (var dbSession = DocumentStoreHolder.Store.OpenSession())
            {
                var requests = dbSession.Query<Request>()
                                        .ToList();

                requests.ForEach(r => dbSession.Delete(r));
                dbSession.SaveChanges();
            }
        }

        private static void AddDivisions()
        {
            using (var dbSession = DocumentStoreHolder.Store.OpenSession())
            {
                var divisions = new List<Division>()
                {
                    new Division { DivisionName = "Core" },
                    new Division { DivisionName = "Raiser" }
                };

                divisions.ForEach(d => dbSession.Store(d));
                dbSession.SaveChanges();
            }
        }

        private static void AddEmailTemplates(int requestId, string templateName, string templateDesc, string toAddress)
        {
            using (var dbSession = DocumentStoreHolder.Store.OpenSession())
            {
                var emailTemplate =
                    new EmailTemplate
                    {
                        RequestId = requestId,//33,
                        TemplateName = templateName,//"Core-Reset",
                        Description = templateDesc, //"Core-Reset email template",
                        To = new List<string> { toAddress },//"samik.dam@jamesriverins.com" },
                        EmailTemplateBody = "Test",
                    };

                List<EmailTemplateField> templateFields = GetTemplateFields(out List<FieldOption> locationOptions, out EmailTemplateField location);
                dbSession.Store(emailTemplate);

                templateFields.ForEach(f =>
                {
                    f.EmailTemplateId = emailTemplate.Id;
                    dbSession.Store(f);
                });

                emailTemplate.TemplateFieldIds = templateFields.Select(t => t.Id).ToList();

                locationOptions.ForEach(lo =>
                {
                    lo.TemplateFieldId = location.Id;
                    dbSession.Store(lo);
                });

                location.FieldOptionsIds = locationOptions.Select(loc => loc.Id).ToList();
                dbSession.SaveChanges();
            }
        }

        private static List<EmailTemplateField> GetTemplateFields(out List<FieldOption> locationOptioins, out EmailTemplateField locationField)
        {
            locationField = new EmailTemplateField
            {
                FieldName = "Location",
                DisplayName = "Location",
                DataType = "String",
                FieldOrder = 4,
                FieldType = "DropDown",
                IsAllowBlank = true,

            };

            locationOptioins = new List<FieldOption>
            {
                new FieldOption() { DisplayName = "Richmond", Value = "Richmond", TemplateFieldId = locationField.Id },
                new FieldOption() { DisplayName = "New York", Value = "New York", TemplateFieldId = locationField.Id }
            };

            var templateFields = new List<EmailTemplateField>()
                {

                    new EmailTemplateField
                    {
                        FieldName = "ClaimNumber",
                        DisplayName = "Claim Number",
                        DataType = "Int",
                        FieldOrder = 1,
                        FieldType = "TextBox",
                        IsAllowBlank = false
                    },
                    //new EmailTemplateField
                    //{
                    //    FieldName = "Submission",
                    //    DisplayName = "Submission Number",
                    //    DataType = "Int",
                    //    FieldOrder = 2,
                    //    FieldType = "TextBox",
                    //    FormatRegEx = "/[0-9]/",
                    //    IsAllowBlank = false
                    //},
                    //new EmailTemplateField
                    //{
                    //    FieldName = "SubmissionVersion",
                    //    DisplayName = "Submission Version",
                    //    DataType = "Int",
                    //    FieldOrder = 3,
                    //    FieldType = "TextBox",
                    //    IsAllowBlank = false
                    //},
                    locationField
                    ,
                    new EmailTemplateField
                    {
                        FieldName = "BrokerName",
                        DisplayName = "Broker Name",
                        DataType = "String",
                        FieldOrder = 5,
                        FieldType = "TextBox",
                        IsAllowBlank = true
                    },

                };

            return templateFields;
        }
    }
}
