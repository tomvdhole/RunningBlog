using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningBlog.Models;

namespace RunningBlog.Services
{
    public interface ICommentServices
    {
        Task SaveComment(Comment comment);
    }
}
