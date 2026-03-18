using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTicketPOC.Common
{
    public static class SessionWrapper
    {
        private static ISessionProvider? _provider;

        public static ISessionProvider Provider
        {
            get
            {
                if (_provider == null)
                    throw new InvalidOperationException("SessionWrapper.Provider has not been initialized. Call SessionWrapper.Initialize() before use.");
                return _provider;
            }
        }

        public static void Initialize(ISessionProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public static void Set<T>(string key, T value)
        {
            Provider.Set(key, value!);
        }

        public static T Get<T>(string key)
        {
            var value = Provider.Get(key);
            if (value == null)
                return default(T)!;
            return (T)value;
        }
    }
}

