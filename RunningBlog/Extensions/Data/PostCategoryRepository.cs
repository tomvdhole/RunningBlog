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
            runningBlogDbContext.Set<PostCategory>().Remove(entity);
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

        //Many to many problem solution
        public async Task<PostCategory> Get(int leftTableId, int rightTableID)
        {
            return await runningBlogDbContext.Set<PostCategory>().SingleOrDefaultAsync(pc => pc.PostId == leftTableId && pc.CategoryId == rightTableID);
        }

        public async Task<List<PostCategory>> GetAllWithSameIdAsync(int id)
        {
            return await runningBlogDbContext.Set<PostCategory>().Where<PostCategory>(pc => pc.PostId == id).ToListAsync<PostCategory>();
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
