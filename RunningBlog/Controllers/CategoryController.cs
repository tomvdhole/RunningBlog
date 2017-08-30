using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunningBlog.Data;
using RunningBlog.Services;
using RunningBlog.Models;

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
            return View();
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



    }
}
