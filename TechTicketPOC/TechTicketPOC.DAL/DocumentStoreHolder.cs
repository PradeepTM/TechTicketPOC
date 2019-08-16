using Raven.Client;
using Raven.Client.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTicketPOC.DAL
{
    public static class DocumentStoreHolder
    {
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store
        {
            get { return store.Value; }
        }

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
            {
                //Url = "http://localhost:8080",
                ConnectionStringName = "TechTicketServer",
                DefaultDatabase = "TechTicket"
            }.Initialize();

            return store;
        }
    }

}
