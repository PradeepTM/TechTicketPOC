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

            if (Current.Session.Keys.Cast<string>().Contains(key))
                Current.Session[key] = value;
            else
                Current.Session.Add(key, value);

        }

        public static T Get<T>(string key)
        {

            if (Current.Session.Keys.Cast<string>().Contains(key))
                return (T)Current.Session[key];
            else
                return default(T);

        }

    }

}
