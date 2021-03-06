﻿using Microsoft.AspNetCore.Http;
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
        public Post Post { get; set; } 
        [Display(Name = "Foto")]
        public IFormFile Photo { get; set; }

        public List<string> SelectedCategories { get; set; } = new List<string>();
        [Display(Name = "Categories")]
        public SelectList CategorySelectList { get; set; }

        public String Error { get; set; }


        public void PopulateCategories(List<Category> categories)
        {           
            if(categories != null && categories.Count > 0)
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
