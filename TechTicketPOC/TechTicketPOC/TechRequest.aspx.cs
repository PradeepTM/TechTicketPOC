using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechTicketPOC.BLL;
using TechTicketPOC.Entities;

namespace TechTicketPOC
{
    public partial class TechRequest : System.Web.UI.Page
    {
        public EmailTemplateDTO EmailTemplate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            EmailTemplate = new EmailTemplateBLL().GetEmailTemplate(33);
        }
    }
}