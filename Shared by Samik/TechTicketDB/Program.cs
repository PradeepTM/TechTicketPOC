using Raven.Client;
using Raven.Client.Connection;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTicketDB
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // without specifying `DefaultDatabase`
                // created `DatabaseCommands` or opened `Sessions`
                // will work on `<system>` database by default
                // if no database is passed explicitly
                using (IDocumentStore store = new DocumentStore
                {
                    //Url = "http://localhost:8080/"
                    ConnectionStringName = "TechTicketServer"
                
                }.Initialize())
                {
                    //IDatabaseCommands commands = store.DatabaseCommands;
                   

                    //IDatabaseCommands Commands = commands.ForDatabase("TechTicket");
                    
                    // using (IDocumentSession session = store.OpenSession("TechTicket"))
                    using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
                    {
                        //session.Advanced.Clear();
                        try
                        {
                            // generate Id automatically
                            // when database is new and empty database and conventions are not changed: 'employee/1'
                            //division obj = new division()
                            //{

                            //    division_name = "Core"


                            //};


                            //IList<division> results = session
                            //    .Query<division>().Where(x=>x.division_name =="Core")
                            //    .ToList(); // send query

                            //request rq = session
                            //   .Query<request>().Where(x => x.request_name == "PIP").SingleOrDefault();

                            //request rqc = session
                            //   .Include<request>(x=>x.parent_request_id).Load(225);

                            //request rqp = session
                            //  .Load<request>(rqc.parent_request_id);



                            //template obj = new template()
                            //{


                            //    request_id = rq.Id,
                            //    template_name = "PIP_Template",
                            //    template_des ="This is template for PIP",
                            //    template_body ="<html>PIP</html>",
                            //    fields_id = new List<int>()
                            //    {
                            //        6,7
                            //    }


                            //};

                            field obj = new field()
                            {

                                template_id = 33,
                                display_name = "Fax Title:",
                                field_name = "txtTitle_FAX",
                                field_type = "TEXTBOX",
                                field_order = 2,
                                default_value = "Please enter Title",
                                allow_blank = false,
                                data_type = "STRING"

                            };

                            session.Store(obj);

                            //string id = session.Advanced.GetDocumentId(obj);



                            // send all pending operations to server, in this case only `Put` operation
                            session.SaveChanges();

                           

                        }
                        catch(Exception ex)
                        {
                             session.Advanced.Clear();
                        }

                        //List<tbl_master_division> divisionMaster = session.Load<tbl_master_division>().ToList();

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
