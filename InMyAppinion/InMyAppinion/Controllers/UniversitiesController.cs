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
    public class UniversitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UniversitiesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: University
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.University.Include(u => u.City);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: University/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.University
                .Include(u => u.City)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // GET: University/Create
        public IActionResult Create()
        {
            ViewData["CityID"] = new SelectList(_context.City, "ID", "Name");
            return View();
        }

        // POST: University/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,ShortName,CityID")] University university)
        {
            if (ModelState.IsValid)
            {
                _context.Add(university);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CityID"] = new SelectList(_context.City, "ID", "Name", university.CityID);
            return View(university);
        }

        // GET: University/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.University.SingleOrDefaultAsync(m => m.ID == id);
            if (university == null)
            {
                return NotFound();
            }
            ViewData["CityID"] = new SelectList(_context.City, "ID", "Name", university.CityID);
            return View(university);
        }

        // POST: University/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ShortName,CityID")] University university)
        {
            if (id != university.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(university);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversityExists(university.ID))
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
            ViewData["CityID"] = new SelectList(_context.City, "ID", "Name", university.CityID);
            return View(university);
        }

        // GET: University/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var university = await _context.University
                .Include(u => u.City)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        // POST: University/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var university = await _context.University.SingleOrDefaultAsync(m => m.ID == id);
            _context.University.Remove(university);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UniversityExists(int id)
        {
            return _context.University.Any(e => e.ID == id);
        }
    }
}
