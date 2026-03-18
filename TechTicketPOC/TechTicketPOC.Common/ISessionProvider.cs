using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTicketPOC.Common
{
    public interface ISessionProvider
    {
        void Set(string key, object value);
        object Get(string key);
        bool ContainsKey(string key);
    }
}
