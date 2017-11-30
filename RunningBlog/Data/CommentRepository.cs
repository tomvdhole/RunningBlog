using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RunningBlog.Models;

namespace RunningBlog.Data
{
    public class CommentRepository: IRepository<Comment>
    {
        private RunningBlogDbContext runningBlogDbContext;

        public CommentRepository(RunningBlogDbContext context)
        {
            this.runningBlogDbContext = context;
        }

        public async Task AddAsync(Comment entity)
        {
            await runningBlogDbContext.Comment.AddAsync(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }

        public async Task Delete(Comment entity)
        {
            runningBlogDbContext.Comment.Remove(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }

        public async Task<Comment> Get(Comment entity)
        {
            return await runningBlogDbContext.Comment.SingleOrDefaultAsync<Comment>(c => c.Id == entity.Id);
        }

        public async Task<Comment> Get(int id)
        {
            return await runningBlogDbContext.Comment.SingleOrDefaultAsync<Comment>(c => c.Id == id);
        }

        //Many to many problem solution
        public Task<Comment> Get(int leftTableId, int rightTableID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllWithSameIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
