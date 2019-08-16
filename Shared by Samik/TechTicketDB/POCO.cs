using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTicketDB
{
    public class division
    {
        /// <summary>
        /// This field for dynamic generation of unique document id  by RavenDB server.
        /// </summary>
        public int Id { get; set; }
      
        public string division_name { get; set; }


    }

    public class request
    {
        /// <summary>
        /// This field for dynamic generation of unique document id  by RavenDB server.
        /// </summary>
        public int Id { get; set; }
        
        public string request_name { get; set; }

        public int division_id { get; set; }

        public int parent_request_id { get; set; }
    }

    public class template
    {
        /// <summary>
        /// This field for dynamic generation of unique document id  by RavenDB server.
        /// </summary>
        public int Id { get; set; }
        

        public int request_id { get; set; }

        public string template_name { get; set; }
        public string template_des { get; set; }

        public string template_body { get; set; }

        public List<int> fields_id { get; set; }
    }

    public class field
    {
        /// <summary>
        /// This field for dynamic generation of unique document id  by RavenDB server.
        /// </summary>
        public int Id { get; set; }
    

        public int template_id { get; set; }

        public string field_name { get; set; }

        public string display_name { get; set; }

        public string data_type { get; set; }

        public string field_type { get; set; }

        public bool allow_blank { get; set; }

        public int field_order { get; set; }

        public string min_value { get; set; }

        public string max_value { get; set; }

        public string format_reg_ex { get; set; }

        public string default_value { get; set; }

    }

   

    public class email_transaction_log
    {
        /// <summary>
        /// This field for dynamic generation of unique document id  by RavenDB server.
        /// </summary>
        public string Id { get; set; }
   
        public string template_id { get; set; }

        public string from { get; set; }

        public string to { get; set; }

        public string cc { get; set; }

        public string email_body { get; set; }

        public DateTime sent_on { get; set; }

        public string sent_by { get; set; }

    }
}
