using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Evidences;

namespace TrafficReports.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;

        public CommentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Comment
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Comments.Include(c => c.Account).Include(c => c.ParentComment).Include(c => c.VehicleViolation);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Comment/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Account)
                .Include(c => c.ParentComment)
                .Include(c => c.VehicleViolation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comment/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ParentCommentId"] = new SelectList(_context.Comments, "Id", "Id");
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id");
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentText,CreatedAt,AccountId,VehicleViolationId,Id")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Id = Guid.NewGuid();
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Users, "Id", "Id", comment.AccountId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", comment.VehicleViolationId);
            return View(comment);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateChildComment([Bind("CommentText,CreatedAt,ParentCommentId,AccountId,VehicleViolationId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Id = Guid.NewGuid();
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the model state is not valid, reload the dropdown list for ParentCommentId
            ViewData["ParentCommentId"] = new SelectList(_context.Comments, "Id", "CommentText", comment.ParentCommentId);
            ViewData["AccountId"] = new SelectList(_context.Users, "Id", "Id", comment.AccountId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", comment.VehicleViolationId);
            return View(comment);
        }

        // GET: Comment/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Users, "Id", "Id", comment.AccountId);
            ViewData["ParentCommentId"] = new SelectList(_context.Comments, "Id", "Id", comment.ParentCommentId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", comment.VehicleViolationId);
            return View(comment);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CommentText,CreatedAt,ParentCommentId,AccountId,VehicleViolationId,Id")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Users, "Id", "Id", comment.AccountId);
            ViewData["ParentCommentId"] = new SelectList(_context.Comments, "Id", "Id", comment.ParentCommentId);
            ViewData["VehicleViolationId"] = new SelectList(_context.VehicleViolations, "Id", "Id", comment.VehicleViolationId);
            return View(comment);
        }

        // GET: Comment/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Account)
                .Include(c => c.ParentComment)
                .Include(c => c.VehicleViolation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
