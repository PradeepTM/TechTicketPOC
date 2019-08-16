using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTicketDB
{
    public class Repository
    {
        public static List<division> GetAllDivision()
        {

            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                //session.Advanced.Clear();
                try
                {


                    List<division> results = session
                        .Query<division>()
                        .ToList(); // send query

                    //session.Store(results);          



                    // send all pending operations to server, in this case only `Put` operation
                    //session.SaveChanges();

                    return results;



                }
                catch (Exception ex)
                {

                    session.Advanced.Clear();
                    return new List<division>();
                }

                //List<tbl_master_division> divisionMaster = session.Load<tbl_master_division>().ToList();

            }

        }

        public static List<request> GetAllRequestByDivisionId(int division_id)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                //session.Advanced.Clear();
                try
                {
                    List<request> results = session
                        .Query<request>().Where(x => x.division_id == division_id)
                        .ToList(); // send query

                    //session.Store(results);       

                    // send all pending operations to server, in this case only `Put` operation
                    //session.SaveChanges();

                    return results;



                }
                catch (Exception ex)
                {

                    session.Advanced.Clear();
                    return new List<request>();
                }
            }
        }

        public static ViewModelDTO GetAllFieldAndTemplateByRequestId(int request_id)
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                //session.Advanced.Clear();
                try
                {
                    template templateResults = session
                       .Query<template>().Where(x => x.request_id == request_id).SingleOrDefault();
                    

                    List<field> fieldResults = session
                       .Query<field>().Where(x => x.template_id == templateResults.Id)
                       .ToList(); // send query

                    ViewModelDTO vm = new ViewModelDTO()
                    {
                        template = templateResults,
                        fields = fieldResults

                    };

                    //session.Store(results);       

                    // send all pending operations to server, in this case only `Put` operation
                    //session.SaveChanges();

                    return vm;



                }
                catch (Exception ex)
                {

                    session.Advanced.Clear();
                    return new ViewModelDTO();
                }

            }

        }
    }
}
