﻿using RunningBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Services
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetCategories();
        Task SaveCategory(Category category);
        Task<Category> GetCategory(int id);
        Task<Category> GetCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
    }
}
