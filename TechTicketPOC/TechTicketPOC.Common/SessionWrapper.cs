using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Web.HttpContext;
using System.Web.SessionState;

namespace TechTicketPOC.Common
{
    public static class SessionWrapper
    {

        public static void Set<T>(string key, T value)
        {
            Current.Session[key] = value;
        }

        public static T Get<T>(string key)
        {
            var value = Current.Session[key];
            return value != null ? (T)value : default(T);
        }

    }

}
