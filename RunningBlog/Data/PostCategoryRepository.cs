using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningBlog.Models;
using Microsoft.EntityFrameworkCore;



namespace RunningBlog.Data
{
    public class PostCategoryRepository : IRepository<PostCategory>
    {
        private RunningBlogDbContext runningBlogDbContext;

        public PostCategoryRepository(RunningBlogDbContext runningBlogDbContext)
        {
            this.runningBlogDbContext = runningBlogDbContext;
        }

        public async Task AddAsync(PostCategory entity)
        {
            await runningBlogDbContext.Set<PostCategory>().AddAsync(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }

        public async Task Delete(PostCategory entity)
        {
            await runningBlogDbContext.Set<PostCategory>().Remove(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }

        public Task<PostCategory> Get(PostCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task<PostCategory> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task Update(PostCategory entity)
        {
            throw new NotImplementedException();
        }
    }
}
