using Microsoft.EntityFrameworkCore;
using RunningBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Data
{
    public class CategoryRepository : IRepository<Category>
    {
        private RunningBlogDbContext runningBlogDbContext;

        public CategoryRepository(RunningBlogDbContext context)
        {
            this.runningBlogDbContext = context;
        }

        public async Task AddAsync(Category entity)
        {
            await runningBlogDbContext.Category.AddAsync(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }

        public Task Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> Get(Category entity)
        {
            return await runningBlogDbContext.Category.SingleOrDefaultAsync<Category>(cRepository => cRepository.Name == entity.Name);
        }

        public Task<Category> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await runningBlogDbContext.Category.ToListAsync<Category>();
        }

        public Task Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
