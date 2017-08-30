using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningBlog.Models;
using RunningBlog.Data;

namespace RunningBlog.Services
{
    public class CommentServices : ICommentServices
    {
        private IRepository<Comment> commentRepository;

        public CommentServices(IRepository<Comment> repository)
        {
            this.commentRepository = repository; 
        }

        public async Task SaveComment(Comment comment)
        {
            await commentRepository.AddAsync(comment);
        }
    }
}
