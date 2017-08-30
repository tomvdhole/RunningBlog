using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Display(Name = "Commentaar inhoud"), Required(ErrorMessage = "Inhoud is verplicht!"), DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public string PublishedOn { get; set; }
        public string PublishedBy { get; set; }

        //A comment belongs to one Post
        public int PostId { get; set; }
        public Post Post { get; set; }


    }
}
