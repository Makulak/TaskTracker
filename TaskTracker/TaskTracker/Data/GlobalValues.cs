using System.Runtime.InteropServices.ComTypes;
using TaskTracker.Models;

namespace TaskTracker.Data
{
    internal static class GlobalValues
    {
        public static string RestUrl => @"http://issuetracking-env.wdjmtyt9xr.us-east-2.elasticbeanstalk.com/";

        public static User LoggedUser;
    }
}
