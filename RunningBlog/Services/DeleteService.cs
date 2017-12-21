using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Services
{
    public class DeleteService : IDeleteService
    {
        public bool MayDelete(string loggedOnUser, string commentOwner, bool isAdmin, bool isPowerUser)
        {
            if(loggedOnUser == commentOwner || isAdmin == true || isPowerUser == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
