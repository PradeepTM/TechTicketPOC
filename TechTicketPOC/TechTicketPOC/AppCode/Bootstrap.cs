using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechTicketPOC.AppCode
{
    public static class Bootstrap
    {
        public static void ConfigureAutomapper()
        {
            Mapper.Initialize((config) => config.AddProfiles("TechTicketPOC", "TechTicketPOC.DAL"));
        }
    }
}