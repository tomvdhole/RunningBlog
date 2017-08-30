using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using RunningBlog.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Models.PostViewModels
{
    public class CreatePostViewModel
    {       
        public Post Post { get; set; } = new Post();
        [Display(Name = "Foto")]
        public IFormFile Photo { get; set; }

        public List<string> SelectedCategories { get; set; }
        [Display(Name = "Categories")]
        public SelectList CategorySelectList { get; set; } 


        public async Task PopulateCategories(ICategoryServices categoryServices)
        {
            List<Category> categories = await categoryServices.GetCategories();
            
            if(categories != null)
            {
                List<SelectListItem> selectListItems = new List<SelectListItem>();

                foreach (Category category in categories)
                {
                    selectListItems.Add(new SelectListItem { Text = category.Name, Value = category.Name });
                }

                CategorySelectList = new SelectList(selectListItems, "Value", "Text");
            }
        }
    }
}
