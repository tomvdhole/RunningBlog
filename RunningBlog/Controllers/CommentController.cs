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

        private void PrepareCommentToSave(Comment comment)
        {
            comment.PublishedBy = User.Identity.Name;
            comment.PublishedOn = DateTime.Now.ToString();
        }
    }
}

        //    // GET: Comment
        //    public async Task<IActionResult> Index()
        //    {
        //        var runningBlogDbContext = _context.Comment.Include(c => c.Post);
        //        return View(await runningBlogDbContext.ToListAsync());
        //    }

        //    // GET: Comment/Details/5
        //    public async Task<IActionResult> Details(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var comment = await _context.Comment
        //            .Include(c => c.Post)
        //            .SingleOrDefaultAsync(m => m.Id == id);
        //        if (comment == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(comment);
        //    }

        //    // GET: Comment/Create
        //    public IActionResult Create()
        //    {
        //        ViewData["PostId"] = new SelectList(_context.Post, "Id", "Content");
        //        return View();
        //    }

        //    // POST: Comment/Create
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create([Bind("Id,Content,PublishedOn,PublishedBy,PostId")] Comment comment)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _context.Add(comment);
        //            await _context.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }
        //        ViewData["PostId"] = new SelectList(_context.Post, "Id", "Content", comment.PostId);
        //        return View(comment);
        //    }

        //    // GET: Comment/Edit/5
        //    public async Task<IActionResult> Edit(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var comment = await _context.Comment.SingleOrDefaultAsync(m => m.Id == id);
        //        if (comment == null)
        //        {
        //            return NotFound();
        //        }
        //        ViewData["PostId"] = new SelectList(_context.Post, "Id", "Content", comment.PostId);
        //        return View(comment);
        //    }

        //    // POST: Comment/Edit/5
        //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Edit(int id, [Bind("Id,Content,PublishedOn,PublishedBy,PostId")] Comment comment)
        //    {
        //        if (id != comment.Id)
        //        {
        //            return NotFound();
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                _context.Update(comment);
        //                await _context.SaveChangesAsync();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!CommentExists(comment.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //            return RedirectToAction("Index");
        //        }
        //        ViewData["PostId"] = new SelectList(_context.Post, "Id", "Content", comment.PostId);
        //        return View(comment);
        //    }

        //    // GET: Comment/Delete/5
        //    public async Task<IActionResult> Delete(int? id)
        //    {
        //        if (id == null)
        //        {
        //            return NotFound();
        //        }

        //        var comment = await _context.Comment
        //            .Include(c => c.Post)
        //            .SingleOrDefaultAsync(m => m.Id == id);
        //        if (comment == null)
        //        {
        //            return NotFound();
        //        }

        //        return View(comment);
        //    }

        //    // POST: Comment/Delete/5
        //    [HttpPost, ActionName("Delete")]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> DeleteConfirmed(int id)
        //    {
        //        var comment = await _context.Comment.SingleOrDefaultAsync(m => m.Id == id);
        //        _context.Comment.Remove(comment);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    private bool CommentExists(int id)
        //    {
        //        return _context.Comment.Any(e => e.Id == id);
        //    }
        //
