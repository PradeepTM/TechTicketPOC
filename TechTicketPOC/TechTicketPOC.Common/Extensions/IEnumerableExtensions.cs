using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTicketPOC.Common.Extensions
{
    public static class IEnumerableExtensions
    {

        public static bool IsCollectionValid<T>(this IEnumerable<T> collection)
        {
            return collection.IsNotNull() && collection.Any();
        }

    }
}
