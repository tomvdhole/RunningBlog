using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Services
{
    public interface IDeleteService
    {
        bool MayDelete(string loggedOnUser, string commentOwner, bool isAdmin, bool isPowerUser);
    }
}
