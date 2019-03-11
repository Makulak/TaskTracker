using System;
using System.Text;
using TaskTracker.Data;

namespace TaskTracker.Helper
{
    public static class UriFactory
    {
        public static Uri CreateEndpointUri(string endpoint)
        {
            return new Uri(GlobalValues.RestUrl + endpoint);
        }
    }
}
