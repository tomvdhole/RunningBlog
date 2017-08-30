using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunningBlog.Data;
using RunningBlog.Models;
using RunningBlog.Services;
using RunningBlog.Models.PostViewModels;

namespace RunningBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostServices postServices;

        public PostController(IPostServices postServices)
        {
            this.postServices = postServices;
        }

     
        public async Task<IActionResult> Index()
        {
            List<Post> posts = await postServices.GetPosts();
            return View(posts);
        }


        // GET: Post/Create
        public async Task<IActionResult> Create([FromServices] ICategoryServices categoryServices)
        {
            return View(await ConfigureCreatePostViewModel(categoryServices));
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Post, Photo, SelectedCategories")] CreatePostViewModel createPostViewModel, 
                                                [FromServices] IPostCategoryServices postCategoryServices, 
                                                [FromServices] ICategoryServices categoryServices)
        {
            if(ModelState.IsValid)
            {
                await postServices.SavePost(createPostViewModel.Post, createPostViewModel.Photo);
                await SavePostCategory(createPostViewModel, postCategoryServices, categoryServices);
                return RedirectToAction("Index");
            }
            return View(createPostViewModel);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await postServices.Get(id.Value);
            if (post == null)
            {
                return NotFound();
            }
            CreatePostViewModel createPostViewModel = new CreatePostViewModel();
            createPostViewModel.Post = post;
            return View(createPostViewModel);
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Post, Photo")] CreatePostViewModel createPostViewModel,
                                              [FromServices] IPostCategoryServices postCategoryServices,
                                              [FromServices] ICategoryServices categoryServices)
        {
            if (ModelState.IsValid)
            {
                await postServices.UpdatePost(createPostViewModel.Post, createPostViewModel.Photo);
                await UpdatePostCategory(createPostViewModel, postCategoryServices, categoryServices);
                return RedirectToAction("Index");
            }
            return View(createPostViewModel);
        }

        

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await postServices.Get(id.Value);
              
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }



        //    

        //    



        //    // GET: Post/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var post = await _context.Post
        //            .SingleOrDefaultAsync(m => m.Id == id);
        //        if (post == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(post);
        //    }

        //    // POST: Post/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var post = await _context.Post.SingleOrDefaultAsync(m => m.Id == id);
        //        _context.Post.Remove(post);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    private bool PostExists(int id)
        //    {
        //        return _context.Post.Any(e => e.Id == id);
        //    }
        //}

        #region Private Methods
        private async Task<CreatePostViewModel> ConfigureCreatePostViewModel(ICategoryServices categoryServices)
        {
            CreatePostViewModel createPostViewModel = new CreatePostViewModel();
            createPostViewModel.Post.PublishedBy = User.Identity.Name;
            await createPostViewModel.PopulateCategories(categoryServices);
            return createPostViewModel;
        }

        private async Task SavePostCategory(CreatePostViewModel createPostViewModel, IPostCategoryServices postCategoryServices, ICategoryServices categoryServices)
        {
            foreach (string category in createPostViewModel.SelectedCategories)
            {
                Category cat = await categoryServices.GetCategory(new Category { Name = category });
                PostCategory postCategory = new PostCategory { PostId = createPostViewModel.Post.Id, CategoryId = cat.Id };
                await postCategoryServices.SavePostCategory(postCategory);
            }
        }

        private async Task UpdatePostCategory(CreatePostViewModel createPostViewModel, IPostCategoryServices postCategoryServices, ICategoryServices categoryServices)
        {
          //  await DeletePostCategory(createPostViewModel.Post.Id, postCategoryServices);
        }

        //private Task DeletePostCategory(int id, IPostCategoryServices postCategoryServices)
        //{
        //    postCategoryServices.
        //}
        #endregion Private Methods
    }
}
