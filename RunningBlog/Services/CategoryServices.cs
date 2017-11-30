using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningBlog.Models;
using RunningBlog.Data;

namespace RunningBlog.Services
{
    public class CategoryServices : ICategoryServices
    {
        private IRepository<Category> categoryRepository;

        public CategoryServices(IRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task DeleteCategory(Category category)
        {
            await categoryRepository.Delete(category);
        }

        public async Task<List<Category>> GetCategories()
        {
            return await categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await categoryRepository.Get(id);
        }

        public async Task<Category> GetCategory(Category category)
        {
            return await categoryRepository.Get(category);
        }

        public async Task SaveCategory(Category category)
        {
            await categoryRepository.AddAsync(category);
        }

        public async Task UpdateCategory(Category category)
        {
            await categoryRepository.Update(category);
        }
    }
}
