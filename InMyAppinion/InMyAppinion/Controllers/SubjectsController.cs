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
using InMyAppinion.Models.SubjectViewModels;

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
                .Include(s => s.Professors)
                    .ThenInclude(set => set.Professor)
               .Include(s => s.Reviews)
                   .ThenInclude(r => r.Author)
               .Include(s => s.Reviews)
                   .ThenInclude(r => r.SubjectReviewTagSet)
                       .ThenInclude(set => set.SubjectReviewTag)
               .Include(s => s.Reviews)
                   .ThenInclude(r => r.Comments)
               .Include(s => s.SubjectTagSet)
                   .ThenInclude(s => s.SubjectTag)
               .Include(s => s.Faculty)
                   .ThenInclude(f => f.University)
                       .ThenInclude(u => u.City)
                           
               .SingleOrDefaultAsync(m => m.ID == id);

            if (subject == null)
            {
                return NotFound();
            }
            var diff = calcDifficulty(subject.Reviews);
            var interes = calcInterest(subject.Reviews);
            var usefulness = calcUsefulness(subject.Reviews);
            var total = calcTotalGrade(subject.Reviews);

            var gradesDict = new Dictionary<string, GradeInfo>();

            gradesDict.Add(
               "Težina",
               new GradeInfo { Grade = diff }
            );

            gradesDict.Add(
               "Predavaèi",
               new GradeInfo { Grade = interes }
            );

            gradesDict.Add(
              "Korisnost",
              new GradeInfo { Grade = usefulness }
            );

            gradesDict.Add(
              "Ukupno",
              new GradeInfo { Grade = total }
            );


            var model = new SubjectDetailViewModel
            {
                ID = subject.ID,
                Name = subject.Name,
                ShortName = subject.ShortName,
                Faculty = subject.Faculty,
                Reviews = subject.Reviews.OrderBy(r => r.Points).Take(2).ToList(),
                Description = subject.Description,
                Professors = subject.Professors.Select(p => p.Professor).ToList(),
                SubjectTags = subject.SubjectTagSet.Select(set => set.SubjectTag).ToList(),
                Validated = subject.Validated,
                AvgDifficultyGrade = diff,
                AvgInterestGrade = interes,
                AvgUsefulnessGrade = usefulness,
                AvgTotal = total,
                Grades = gradesDict
            };

            return View(model);
        }

        private double calcDifficulty(ICollection<SubjectReview> reviews)
        {
            return reviews.Count() != 0 ? Math.Round(reviews.Select(r => r.DifficultyGrade).Average(), 2) : 0;
        }

        private double calcInterest(ICollection<SubjectReview> reviews)
        {
            return reviews.Count() != 0 ? Math.Round(reviews.Select(r => r.InterestGrade).Average(), 2) : 0;
        }

        private double calcUsefulness(ICollection<SubjectReview> reviews)
        {
            return reviews.Count() != 0 ? Math.Round(reviews.Select(r => r.UsefulnessGrade).Average(), 2) : 0;
        }


        private double calcTotalGrade(ICollection<SubjectReview> reviews)
        {
            return reviews.Count() != 0 ? Math.Round((double)reviews.Select(r => r.TotalGrade).Average(), 2) : 0;
        }

        // GET: Subjects/Create
        [Authorize(Roles = "Korisnik")]
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
