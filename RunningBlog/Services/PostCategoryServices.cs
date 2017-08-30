using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningBlog.Models;
using RunningBlog.Data;

namespace RunningBlog.Services
{
    public class PostCategoryServices : IPostCategoryServices
    {
        private IRepository<PostCategory> repository;

        public PostCategoryServices(IRepository<PostCategory> repository)
        {
            this.repository = repository;
        }

        public Task DeletePostCategory(PostCategory postCategory)
        {
            throw new NotImplementedException();
        }

        public async Task SavePostCategory(PostCategory postCategory)
        {
            await repository.AddAsync(postCategory);
        }
    }
}
