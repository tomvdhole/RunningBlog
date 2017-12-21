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

        public async Task Delete(Category entity)
        {
            runningBlogDbContext.Category.Remove(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }

        public async Task<Category> Get(Category entity)
        {
            return await runningBlogDbContext.Category.SingleOrDefaultAsync<Category>(cRepository => cRepository.Name == entity.Name);
        }

        public async Task<Category> Get(int id)
        {
            Category category = await runningBlogDbContext.Category.SingleOrDefaultAsync<Category>(cRepository => cRepository.Id == id);
            return category;
        }

        //Many to many problem solution
        public Task<Category> Get(int leftTableId, int rightTableID) 
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await runningBlogDbContext.Category.ToListAsync<Category>();
        }

        public Task<List<Category>> GetAllWithSameIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Category entity)
        {

            Category origCategory = await runningBlogDbContext.Category.AsNoTracking<Category>().SingleOrDefaultAsync(c => c.Id == entity.Id);
            runningBlogDbContext.Entry<Category>(origCategory).Context.Update<Category>(entity);
            await runningBlogDbContext.SaveChangesAsync();
        }
    }
}
