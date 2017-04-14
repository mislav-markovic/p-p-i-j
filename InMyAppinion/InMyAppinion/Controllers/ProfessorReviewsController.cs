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
    public class ProfessorReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessorReviewsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProfessorReviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProfessorReview.Include(p => p.Author).Include(p => p.Professor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProfessorReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorReview = await _context.ProfessorReview
                .Include(p => p.Author)
                .Include(p => p.Professor)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (professorReview == null)
            {
                return NotFound();
            }

            return View(professorReview);
        }

        // GET: ProfessorReviews/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.User, "Id", "Id");
            ViewData["ProfessorID"] = new SelectList(_context.Professor, "ID", "ID");
            return View();
        }

        // POST: ProfessorReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Text,QualityGrade,InteractionGrade,HelpfulnessGrade,MentorGrade,TotalGrade,Points,Timestamp,AuthorID,ProfessorID")] ProfessorReview professorReview)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professorReview);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AuthorID"] = new SelectList(_context.User, "Id", "Id", professorReview.AuthorID);
            ViewData["ProfessorID"] = new SelectList(_context.Professor, "ID", "ID", professorReview.ProfessorID);
            return View(professorReview);
        }

        // GET: ProfessorReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorReview = await _context.ProfessorReview.SingleOrDefaultAsync(m => m.ID == id);
            if (professorReview == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.User, "Id", "Id", professorReview.AuthorID);
            ViewData["ProfessorID"] = new SelectList(_context.Professor, "ID", "ID", professorReview.ProfessorID);
            return View(professorReview);
        }

        // POST: ProfessorReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Text,QualityGrade,InteractionGrade,HelpfulnessGrade,MentorGrade,TotalGrade,Points,Timestamp,AuthorID,ProfessorID")] ProfessorReview professorReview)
        {
            if (id != professorReview.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professorReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorReviewExists(professorReview.ID))
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
            ViewData["AuthorID"] = new SelectList(_context.User, "Id", "Id", professorReview.AuthorID);
            ViewData["ProfessorID"] = new SelectList(_context.Professor, "ID", "ID", professorReview.ProfessorID);
            return View(professorReview);
        }

        // GET: ProfessorReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorReview = await _context.ProfessorReview
                .Include(p => p.Author)
                .Include(p => p.Professor)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (professorReview == null)
            {
                return NotFound();
            }

            return View(professorReview);
        }

        // POST: ProfessorReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professorReview = await _context.ProfessorReview.SingleOrDefaultAsync(m => m.ID == id);
            _context.ProfessorReview.Remove(professorReview);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProfessorReviewExists(int id)
        {
            return _context.ProfessorReview.Any(e => e.ID == id);
        }
    }
}