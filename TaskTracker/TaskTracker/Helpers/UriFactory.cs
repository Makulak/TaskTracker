using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Data;

namespace TaskTracker.Helper
{
    public static class UriFactory
    {
        public static Uri CreateEndpointUri(string endpoint)
        {
            return new Uri(GlobalValues.RestUrl + endpoint);
        }

        public static Uri CreateEndpointUri(string endpoint, Dictionary<string, object> parameters)
        {
            string par = string.Join("/", parameters.Select(x => x.Key + "=" + x.Value.ToString()));

            return new Uri(GlobalValues.RestUrl + endpoint + par);
        }
    }
}
