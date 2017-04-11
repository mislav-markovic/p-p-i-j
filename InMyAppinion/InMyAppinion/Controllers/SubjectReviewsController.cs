using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InMyAppinion.Data;
using InMyAppinion.Models;

namespace InMyAppinion.Controllers
{
    public class SubjectReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectReviewsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: SubjectReviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubjectReview.Include(s => s.Author).Include(s => s.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubjectReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectReview = await _context.SubjectReview
                .Include(s => s.Author)
                .Include(s => s.Subject)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subjectReview == null)
            {
                return NotFound();
            }

            return View(subjectReview);
        }

        // GET: SubjectReviews/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.User, "Id", "Id");
            ViewData["SubjectID"] = new SelectList(_context.Subject, "ID", "ID");
            return View();
        }

        // POST: SubjectReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Text,UsefulnessGrade,InterestGrade,DifficultyGrade,TotalGrade,Points,Timestamp,AuthorID,SubjectID")] SubjectReview subjectReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectReview);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AuthorID"] = new SelectList(_context.User, "Id", "Id", subjectReview.AuthorID);
            ViewData["SubjectID"] = new SelectList(_context.Subject, "ID", "ID", subjectReview.SubjectID);
            return View(subjectReview);
        }

        // GET: SubjectReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectReview = await _context.SubjectReview.SingleOrDefaultAsync(m => m.ID == id);
            if (subjectReview == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.User, "Id", "Id", subjectReview.AuthorID);
            ViewData["SubjectID"] = new SelectList(_context.Subject, "ID", "ID", subjectReview.SubjectID);
            return View(subjectReview);
        }

        // POST: SubjectReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Text,UsefulnessGrade,InterestGrade,DifficultyGrade,TotalGrade,Points,Timestamp,AuthorID,SubjectID")] SubjectReview subjectReview)
        {
            if (id != subjectReview.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectReviewExists(subjectReview.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["AuthorID"] = new SelectList(_context.User, "Id", "Id", subjectReview.AuthorID);
            ViewData["SubjectID"] = new SelectList(_context.Subject, "ID", "ID", subjectReview.SubjectID);
            return View(subjectReview);
        }

        // GET: SubjectReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectReview = await _context.SubjectReview
                .Include(s => s.Author)
                .Include(s => s.Subject)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subjectReview == null)
            {
                return NotFound();
            }

            return View(subjectReview);
        }

        // POST: SubjectReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subjectReview = await _context.SubjectReview.SingleOrDefaultAsync(m => m.ID == id);
            _context.SubjectReview.Remove(subjectReview);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SubjectReviewExists(int id)
        {
            return _context.SubjectReview.Any(e => e.ID == id);
        }
    }
}
