using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunningBlog.Data;
using RunningBlog.Models;
using RunningBlog.Services;

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
        public IActionResult Create(int postId)
        {
            var comment = new Comment { PostId = postId};
            return View(comment);
        }

        // POST: Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Comment comment = await commentServices.GetComment(id);
            await commentServices.DeleteComment(comment);
            return RedirectToAction("Details", "Post", new { id = comment.PostId });      
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
