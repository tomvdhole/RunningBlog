using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RunningBlog.Models;
using RunningBlog.Services;
using Microsoft.AspNetCore.Authorization;

namespace RunningBlog.Controllers
{
    public class CommentController : Controller
    {
        private ICommentServices commentServices;

        public CommentController(ICommentServices services)
        {
            commentServices = services;
        }

        //GET: Comment/Create/1
        [Authorize]
        public IActionResult Create(int postId)
        {
            var comment = new Comment { PostId = postId};
            return View(comment);
        }

        // POST: Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Content,PublishedOn,PublishedBy,PostId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                PrepareCommentToSave(comment);
                await commentServices.SaveComment(comment);
                return RedirectToAction("Details", "Post", new { id = comment.PostId });
            }
            return View(comment);
        }

        // GET: Comment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment comment = await commentServices.GetComment(id.Value);

            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,
                                                         [FromServices] IDeleteService deleteService)
        {
            
            Comment comment = await commentServices.GetComment(id);
            bool mayDelete = deleteService.MayDelete(User.Identity.Name, comment.PublishedBy, User.IsInRole("Admin"), User.IsInRole("PowerUser"));
            if (mayDelete == true)
            {
                await commentServices.DeleteComment(comment);
            }
            else
            {
                return RedirectToAction("Details", "Post", new { id = comment.PostId, error = "You have not the rights to delete other persons comments!" });
            }
            return RedirectToAction("Details", "Post", new { id = comment.PostId, error = ""});      
        }

        private void PrepareCommentToSave(Comment comment)
        {
            comment.PublishedBy = User.Identity.Name;
            comment.PublishedOn = DateTime.Now.ToString();
        }
    }
}

        
        

        

        
       

        

        //    private bool CommentExists(int id)
        //    {
        //        return _context.Comment.Any(e => e.Id == id);
        //    }
        //
