using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningBlog.Models;
using RunningBlog.Data;
using Microsoft.EntityFrameworkCore;

namespace RunningBlog.Services
{
    public class PostCategoryServices : IPostCategoryServices
    {
        private IRepository<PostCategory> repository;

        public PostCategoryServices(IRepository<PostCategory> repository)
        {
            this.repository = repository;
        }

        public async Task DeletePostCategory(PostCategory postCategory)
        {
            await repository.Delete(postCategory);
        }

        public async Task<List<PostCategory>> GetAllWithSameIdAsync(int id)
        {
            return await repository.GetAllWithSameIdAsync(id);
        }

        public async Task SavePostCategory(PostCategory postCategory)
        {
            await repository.AddAsync(postCategory);
        }
    }
}
