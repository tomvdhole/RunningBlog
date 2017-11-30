using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningBlog.Models;

namespace RunningBlog.Services
{
    public interface IPostCategoryServices
    {
        Task SavePostCategory(PostCategory postCategory);
        Task DeletePostCategory(PostCategory postCategory);
        Task<List<PostCategory>> GetAllWithSameIdAsync(int id);
    }
}
