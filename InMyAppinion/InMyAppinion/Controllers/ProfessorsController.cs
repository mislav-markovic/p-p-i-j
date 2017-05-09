using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InMyAppinion.Data;
using InMyAppinion.Models;
using InMyAppinion.Models.ProfessorViewModels;

namespace InMyAppinion.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessorsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Professor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Professor.ToListAsync());
        }

        // GET: Professor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .Include(p => p.Reviews)
                .Include(p => p.Subjects).ThenInclude(s => s.Subject)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (professor == null)
            {
                return NotFound();
            }

            var model = new ProfessorDetailViewModel
            {
                ID = professor.ID,
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Reviews = professor.Reviews,
                Subjects = professor.Subjects.Select(set => set.Subject).ToList(),
                Biography = professor.Biography,
                Validated = professor.Validated,
                AvgAccessibility = calcAccessibility(professor.Reviews),
                AvgInteractivity = calcInteractivity(professor.Reviews),
                AvgMentoring = calcMentoring(professor.Reviews),
                AvgTotal = calcTotalGrade(professor.Reviews),
                AvgQuality = calcQuality(professor.Reviews)
            };

            return View(model);
        }

        private double calcQuality(ICollection<ProfessorReview> reviews)
        {
            if (reviews == null || reviews.Count == 0)
            {
                return 0;
            }
            return Math.Round(reviews.Select(r => r.QualityGrade).DefaultIfEmpty().Average(), 2);
        }

        private double calcTotalGrade(ICollection<ProfessorReview> reviews)
        {
            if (reviews == null || reviews.Count == 0)
            {
                return 0;
            }
            var av = reviews.Select(r => r.TotalGrade).DefaultIfEmpty().Average();

            return Math.Round((double)av, 2);
        }

        private double calcMentoring(ICollection<ProfessorReview> reviews)
        {
            if (reviews == null || reviews.Count == 0)
            {
                return 0;
            }
            var av = reviews.Select(r => r.MentorGrade).DefaultIfEmpty().Average();
            if (av == null) return 0;

            return Math.Round((double)av, 2);
        }

        private double calcInteractivity(ICollection<ProfessorReview> reviews)
        {
            if (reviews == null || reviews.Count == 0)
            {
                return 0;
            }
            return Math.Round(reviews.Select(r => r.InteractionGrade).DefaultIfEmpty().Average(), 2);
        }

        private double calcAccessibility(ICollection<ProfessorReview> reviews)
        {
            if (reviews == null || reviews.Count == 0)
            {
                return 0;
            }
            return Math.Round(reviews.Select(r => r.HelpfulnessGrade).DefaultIfEmpty().Average(), 2);
        }

        // GET: Professor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Professor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(professor);
        }

        // GET: Professor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor.SingleOrDefaultAsync(m => m.ID == id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        // POST: Professor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName")] Professor professor)
        {
            if (id != professor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(professor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(professor.ID))
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
            return View(professor);
        }

        // GET: Professor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professor
                .SingleOrDefaultAsync(m => m.ID == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professor.SingleOrDefaultAsync(m => m.ID == id);
            _context.Professor.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProfessorExists(int id)
        {
            return _context.Professor.Any(e => e.ID == id);
        }
    }
}
