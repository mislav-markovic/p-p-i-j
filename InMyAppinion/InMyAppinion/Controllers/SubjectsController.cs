using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InMyAppinion.Data;
using InMyAppinion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace InMyAppinion.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubjectsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        public async Task<IActionResult> Create()
        {
            var currUser = await _userManager.GetUserAsync(User);
            bool isUser;
            if (await _userManager.IsInRoleAsync(currUser, "Administrator") || await _userManager.IsInRoleAsync(currUser, "Moderator"))
            {
                isUser = false;
            }
            else
            {
                isUser = true;
            }

            ViewData["IsUser"] = isUser;
            ViewData["FacultyID"] = new SelectList(_context.Faculty, "ID", "Name");
            ViewData["ProfessorID"] = new SelectList(_context.Professor, "ID", "FullName");
            ViewData["SubjectTag"] = new SelectList(_context.SubjectTag, "ID", "Name");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Korisnik")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ShortName,Description,FacultyID")] Subject subject, bool isUser, ICollection<int> tags, ICollection<int> professorIDs)
        {

            subject.Validated = !isUser;

            var tagSet = new HashSet<SubjectTagSet>();
            var dbTags = _context.SubjectTag.AsNoTracking().Select(t => t.ID).ToList();

            foreach(var tagID in tags)
            {
                if(dbTags.Any(t => t == tagID))
                {
                    tagSet.Add(new SubjectTagSet { SubjectID = subject.ID, SubjectTagID = tagID });
                }
            }

            subject.SubjectTagSet = tagSet;

            var professorSet = new HashSet<ProfessorSubjectSet>();
            var dbProfessors = _context.Professor.AsNoTracking().Select(p => p.ID).ToList();

            foreach(var profID in professorIDs)
            {
                if(dbProfessors.Any(p => p == profID))
                {
                    professorSet.Add(new ProfessorSubjectSet { ProfessorID = profID, SubjectID = subject.ID });
                }
            }

            subject.Professors = professorSet;

            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewData["IsUser"] = isUser;
            ViewData["FacultyID"] = new SelectList(_context.Faculty, "ID", "Name");
            ViewData["ProfessorID"] = new SelectList(_context.Professor, "ID", "FullName");
            ViewData["SubjectTag"] = new SelectList(_context.SubjectTag, "ID", "Name");
            return View();
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
    }
}
