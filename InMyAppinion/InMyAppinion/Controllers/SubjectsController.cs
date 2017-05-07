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
    public class SubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Subject.Include(s => s.Faculty);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .Include(s => s.Faculty)
                .Include(s => s.Reviews)
                .Include(s => s.SubjectTagSet)
                .ThenInclude(t => t.SubjectTag)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            ViewData["FacultyID"] = new SelectList(_context.Faculty, "ID", "ShortName");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,ShortName,Description,FacultyID")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["FacultyID"] = new SelectList(_context.Faculty, "ID", "ID", subject.FacultyID);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject.SingleOrDefaultAsync(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }
            ViewData["FacultyID"] = new SelectList(_context.Faculty, "ID", "ID", subject.FacultyID);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ShortName,Description,FacultyID")] Subject subject)
        {
            if (id != subject.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.ID))
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
            ViewData["FacultyID"] = new SelectList(_context.Faculty, "ID", "ID", subject.FacultyID);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subject
                .Include(s => s.Faculty)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await _context.Subject.SingleOrDefaultAsync(m => m.ID == id);
            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SubjectExists(int id)
        {
            return _context.Subject.Any(e => e.ID == id);
        }

        public IActionResult Validate(int id)
        {
            var subject = _context.Subject.SingleOrDefault(p => p.ID == id);
            if (subject == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    subject.Validated = !subject.Validated;
                    _context.Update(subject);
                    _context.SaveChanges();
                    var result = new
                    {
                        message = $"Predmet {subject.Name} potvrðen.",
                        success = true
                    };
                    return Json(result);
                }
                catch (Exception exc)
                {
                    var result = new
                    {
                        message = $"Pogreška pri ažuriranju: + {exc.InnerException}",
                        success = false
                    };
                    return Json(result);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubjectTagSet(int tagid, int subjectid)
        {
            var subjectTagSet = await _context.SubjectTagSet
                                .SingleOrDefaultAsync(s => s.SubjectID == subjectid && s.SubjectTagID == tagid);

            if (subjectTagSet == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    _context.Remove(subjectTagSet);
                    await _context.SaveChangesAsync();
                    var result = new
                    {
                        message = $"Tag uspješno obrisan.",
                        success = true
                    };
                    return Json(result);
                }
                catch (Exception exc)
                {
                    var result = new
                    {
                        message = $"Pogreška pri brisanju: + {exc.InnerException}",
                        success = false
                    };
                    return Json(result);
                }
            }
        }

        public IActionResult SubjectTagsCloud()
        {
            return View();
        }

        public IActionResult GetCloud()
        {
            List<object> list = new List<object>();
            var tags = _context.SubjectTag.Include(s => s.SubjectTagSet).ToList();
            foreach(var item in tags)
            {
                var result = new
                {
                    text = item.Name,
                    weight = item.SubjectTagSet.Count,
                    link = "../Search/Search?query=" + item.Name
                };
                list.Add(result);
            }

            return Json(list);
        }
    }
}
