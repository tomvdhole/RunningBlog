using Microsoft.EntityFrameworkCore;
using RunningBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Data
{
    public class PostRepository : IRepository<Post>
    {
        private RunningBlogDbContext runningBlogDbContext;

        public PostRepository(RunningBlogDbContext context)
        {
            runningBlogDbContext = context;
        }

        public async Task AddAsync(Post entity)
        {
            await runningBlogDbContext.Post.AddAsync(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }

        public async Task Delete(Post entity)
        {
            runningBlogDbContext.Post.Remove(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }

        public async Task<Post> Get(Post entity)
        {
            return await runningBlogDbContext.Post.Include(post => post.Comments)
                                             .Include(post => post.PostCategories)
                    .SingleOrDefaultAsync<Post>(post => post.Id == entity.Id);
        }

        public async Task<Post> Get(int id)
        {
            return await runningBlogDbContext.Post.Include(post => post.Comments)
                                                  .Include(post => post.PostCategories)
                                                  .ThenInclude(pc => pc.Category)
                    .SingleOrDefaultAsync<Post>(post => post.Id == id);
        }

        public async Task<Post> Get(int leftTableId, int rightTableID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await runningBlogDbContext.Post.OrderByDescending(p => p.Id)
                                            .Include(post => post.Comments)
                                            .Include(post => post.PostCategories)
                   .ToListAsync<Post>();
        }

        public async Task<List<Post>> GetAllWithSameIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Post entity)
        {
            Post origPost = await runningBlogDbContext.Post.AsNoTracking<Post>().SingleOrDefaultAsync(p => p.Id == entity.Id);
            runningBlogDbContext.Entry<Post>(origPost).Context.Update<Post>(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }
    }
}
