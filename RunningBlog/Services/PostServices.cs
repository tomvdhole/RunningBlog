using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RunningBlog.Data;
using RunningBlog.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace RunningBlog.Services
{
    public class PostServices: IPostServices
    {
        private IRepository<Post> postRepository;

        public PostServices(IRepository<Post> repository)
        {
            postRepository = repository;       
        }

        public async Task<List<Post>> GetPosts()
        {
            return await postRepository.GetAllAsync();
        }

        public async Task SavePost(Post post, IFormFile photo)
        {
            await SavePhotoToPost(post, photo);
            post.PublishedOn = DateTime.Now.ToString();
            await postRepository.AddAsync(post);
        }

        public async Task<Post> Get(int id)
        {
            return await postRepository.Get(id);
        }

        public async Task UpdatePost(Post post, IFormFile photo)
        {
            await SavePhotoToPost(post, photo);
            post.LastUpdatedOn = DateTime.Now.ToString();
            await postRepository.Update(post);
        }

        public async Task DeletePost(int id)
        {
            Post post = await postRepository.Get(id);
            await postRepository.Delete(post);
            

        }

        private async Task DeletePostCategories(int id,
                                                [FromServices] IPostCategoryServices postCategoryServices)
        {
            List<PostCategory> postCategories = await postCategoryServices.GetAllWithSameIdAsync(id);
            foreach(PostCategory postCategory in postCategories)
            {
                await postCategoryServices.DeletePostCategory(postCategory);
            }
        }

        #region Private Methods
        private async Task SavePhotoToPost(Post post, IFormFile photo)
        {
            if (photo != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memoryStream);
                    post.Photo = memoryStream.ToArray();
                }
            }
        }
        #endregion Private Methods
    }
}
