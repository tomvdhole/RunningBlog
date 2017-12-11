using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Helpers
{
    public static class SiteRenderer
    {
        public static string NewLineToBR(string replaceString)
        {
            return replaceString.Replace(System.Environment.NewLine, " <br /> ");
        }
    } 
}
