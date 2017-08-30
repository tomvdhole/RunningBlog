using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Many to many implementation
        public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    }
}
