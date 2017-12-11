using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Data
{
    public class InitializationOptions
    {
        public string[] UserNames { get; set; }
        public string[] PassWords { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }
}
