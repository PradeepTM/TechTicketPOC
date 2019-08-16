﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTicketPOC.Entities;

namespace TechTicketPOC.DAL.POCOEntities
{
    public class EmailTemplateField : IAuditable
    {
        public int Id { get; set; }
        public int EmailTemplateId { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
        public string DataType { get; set; }
        public string FieldType { get; set; }
        public bool IsAllowBlank { get; set; }
        public int FieldOrder { get; set; }
        public string DefaultValue { get; set; }
        public List<int> FieldOptionsIds { get; set; }
        public short MaxLength { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public string FormatRegEx { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

}
