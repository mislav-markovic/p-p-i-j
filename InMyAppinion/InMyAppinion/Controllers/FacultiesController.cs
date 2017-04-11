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
    public class FacultiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Faculties
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Faculty.Include(f => f.University);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculty
                .Include(f => f.University)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // GET: Faculties/Create
        public IActionResult Create()
        {
            ViewData["UniversityID"] = new SelectList(_context.University, "ID", "Name");
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,ShortName,UniversityID")] Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faculty);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UniversityID"] = new SelectList(_context.University, "ID", "Name", faculty.UniversityID);
            return View(faculty);
        }

        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculty.SingleOrDefaultAsync(m => m.ID == id);
            if (faculty == null)
            {
                return NotFound();
            }
            ViewData["UniversityID"] = new SelectList(_context.University, "ID", "ID", faculty.UniversityID);
            return View(faculty);
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ShortName,UniversityID")] Faculty faculty)
        {
            if (id != faculty.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.ID))
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
            ViewData["UniversityID"] = new SelectList(_context.University, "ID", "ID", faculty.UniversityID);
            return View(faculty);
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.Faculty
                .Include(f => f.University)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return View(faculty);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculty.SingleOrDefaultAsync(m => m.ID == id);
            _context.Faculty.Remove(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FacultyExists(int id)
        {
            return _context.Faculty.Any(e => e.ID == id);
        }
    }
}
