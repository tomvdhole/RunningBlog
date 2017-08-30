using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RunningBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Display(Name= "Post titel"),Required(ErrorMessage = "Titel is verplicht!")]
        public string Title { get; set; }
        [Display(Name = "Foto")]
        public byte[] Photo { get; set; }
        [Display(Name = "Artikel inhoud"), Required(ErrorMessage = "Inhoud is verplicht!"), DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public string PublishedOn { get; set; }
        public string LastUpdatedOn { get; set; }
        public string PublishedBy { get; set; }

        //A Post can have many Comments
        public List<Comment> Comments { get; set; } = new List<Comment>();

        //Many to many implementation
        public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    }
}
