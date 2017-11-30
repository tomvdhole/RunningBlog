using Microsoft.AspNetCore.Http;
using RunningBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Services
{
    public interface IPostServices
    {
        Task<List<Post>> GetPosts();
        Task SavePost(Post post, IFormFile photo);
        Task<Post> Get(int id);
        Task UpdatePost(Post post, IFormFile photo);
        Task DeletePost(int id);
    }
}
