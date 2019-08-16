﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTicketPOC.Entities;

namespace TechTicketPOC.DAL.POCOEntities
{
    public class FieldOption : IAuditable
    {
        public int Id { get; set; }
        public int TemplateFieldId { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
