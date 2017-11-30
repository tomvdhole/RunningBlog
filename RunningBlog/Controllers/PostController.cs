using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunningBlog.Models;
using RunningBlog.Services;
using RunningBlog.Models.PostViewModels;
using RunningBlog.Models.ManageViewModels;

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
        public async Task<IActionResult> Edit(int? id,
                                              [FromServices] ICategoryServices categoryServices)
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
           
            CreatePostViewModel createPostViewModel =  await ConfigureCreatePostViewModel(categoryServices);
            createPostViewModel.Post = post;

            return View(createPostViewModel);
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Post, Photo, SelectedCategories")] CreatePostViewModel createPostViewModel,
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
        public async Task<IActionResult> Details(int? id,
                                                 [FromServices] ICategoryServices categoryServices)
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

            CreatePostViewModel createPostViewModel = await ConfigureCreatePostViewModel(categoryServices);
            createPostViewModel.Post = post;

            return View(createPostViewModel);
        }

        //    // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,
                                                         [FromServices] IPostCategoryServices postcategoryServices,
                                                         [FromServices] ICommentServices commentServices)
        {
            await postServices.DeletePost(id);
            //two steps underneath not necessary Entity frameworks allready solves it, but it was a good exercise ;-)
            await DeleteExistingPostCategoryEntries(id, postcategoryServices);
            await DeleteExistingPostCommentEntries(id, commentServices);


            return RedirectToAction("Index");
        }

        #region Private Methods
        private async Task DeleteExistingPostCommentEntries(int id, ICommentServices commentServices)
        {
            Post post = await postServices.Get(id);
            if(post != null && post.Comments != null)
            {
                foreach(Comment comment in post.Comments)
                {
                    await commentServices.DeleteComment(comment);
                }
            }
        }

        private async Task<CreatePostViewModel> ConfigureCreatePostViewModel(ICategoryServices categoryServices)
        {
            CreatePostViewModel createPostViewModel = new CreatePostViewModel();
            createPostViewModel.Post.PublishedBy = User.Identity.Name;
            List<Category> categories = await categoryServices.GetCategories();
            createPostViewModel.PopulateCategories(categories);
            return createPostViewModel;
        }

        private async Task SavePostCategory(CreatePostViewModel createPostViewModel, IPostCategoryServices postCategoryServices, ICategoryServices categoryServices)
        {
            if(createPostViewModel.SelectedCategories?.Count > 0)
            {
                foreach (string category in createPostViewModel.SelectedCategories)
                {
                    Category cat = await categoryServices.GetCategory(new Category { Name = category });
                    PostCategory postCategory = new PostCategory { PostId = createPostViewModel.Post.Id, CategoryId = cat.Id };
                    await postCategoryServices.SavePostCategory(postCategory);
                }
            }
        }

        private async Task UpdatePostCategory(CreatePostViewModel createPostViewModel, IPostCategoryServices postCategoryServices, ICategoryServices categoryServices)
        {
            //Delete existing PostCategory entries
            await DeleteExistingPostCategoryEntries(createPostViewModel.Post.Id,  postCategoryServices);
            //Add new PostCategory entries if there are any entries
            if(createPostViewModel.SelectedCategories != null)
            { 
                foreach (string category in createPostViewModel.SelectedCategories)
                {
                    Category cat = await categoryServices.GetCategory(new Category { Name = category });
                    PostCategory postCategory = new PostCategory { PostId = createPostViewModel.Post.Id, CategoryId = cat.Id };
                    await postCategoryServices.SavePostCategory(postCategory);
                }
            }
        }

        private async Task DeleteExistingPostCategoryEntries(int id, IPostCategoryServices postCategoryServices)
        {
            var itemsToDelete = await postCategoryServices.GetAllWithSameIdAsync(id);
            if(itemsToDelete != null && itemsToDelete.Count > 0)
            {
                foreach (var itemToDelete in itemsToDelete)
                {
                    await postCategoryServices.DeletePostCategory(itemToDelete);
                }
            }    
        }
        #endregion Private Methods
    }
}
