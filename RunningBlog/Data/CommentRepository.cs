using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Task Delete(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> Get(Comment entity)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
