using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunningBlog.Services;
using RunningBlog.Models;
using RunningBlog.Models.ManageViewModels;

namespace RunningBlog.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryServices categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await categoryServices.GetCategories());
        }

        // GET: Create
        public IActionResult Create()
        {      
            return View(new Category());
        }


        //POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            if (ModelState.IsValid)
            {

                await categoryServices.SaveCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await categoryServices.GetCategory((int)id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name, Id")] Category category)
        {
            if (ModelState.IsValid)
            {
                await categoryServices.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET: Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await categoryServices.GetCategory((int)id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: /Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,
                                                         [FromServices] IPostCategoryServices postCategoryServices)
        {
            var category = await categoryServices.GetCategory(id);
            await categoryServices.DeleteCategory(category);
            return RedirectToAction("Index");
        }

        // GET: /Details/5
        public async Task<IActionResult> Details(int id)
        {
            var category = await categoryServices.GetCategory(id);
            return View(category);
        }
    }
}
