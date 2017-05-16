using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InMyAppinion.Data;
using InMyAppinion.Models;
using InMyAppinion.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using InMyAppinion.Models.ProfessorViewModels;

namespace InMyAppinion.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public ProfessorsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        // GET: Professor
        public async Task<IActionResult> Index()
        {
            if (await _authorizationService.AuthorizeAsync(User, "CanModerate"))
            {
                return View(await _context.Professor.ToListAsync());
            }
            return View(await _context.Professor.Where(s => s.Validated).ToListAsync());
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
                    .ThenInclude(r => r.Author)
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.ProfessorReviewTagSet)
                        .ThenInclude(set => set.ProfessorReviewTag)
                .Include(p => p.Reviews)
                    .ThenInclude(r => r.Comments)
                .Include(s => s.Subjects)
                    .ThenInclude(s => s.Subject)
                        .ThenInclude(s => s.SubjectTagSet)
                            .ThenInclude(s => s.SubjectTag)
                .Include(s => s.Subjects)
                    .ThenInclude(s => s.Subject)
                        .ThenInclude(s => s.Faculty)
                            .ThenInclude(f => f.University)
                                .ThenInclude(u => u.City)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (professor == null)
            {
                return NotFound();
            }

            var faculties = professor.Subjects.Select(s => s.Subject).Select(s => s.Faculty).Distinct();
            var subjects = professor.Subjects.Select(set => set.Subject);

            var gradesDict = new Dictionary<string, GradeInfo>();

            var AvgAccessibility = calcAccessibility(professor.Reviews);
            gradesDict.Add(
                "Pristupaènost",
                new GradeInfo{Grade = AvgAccessibility}
            );
            var AvgInteractivity = calcInteractivity(professor.Reviews);
            gradesDict.Add(
                "Angažiranost",
                new GradeInfo{Grade = AvgInteractivity}
            );
            var AvgMentoring = calcMentoring(professor.Reviews);
            gradesDict.Add(
                "Mentorstvo",
                new GradeInfo{Grade = AvgMentoring}
            );
            var AvgQuality = calcQuality(professor.Reviews);
            gradesDict.Add(
                "Kvaliteta",
                new GradeInfo{Grade = AvgQuality}
            );
            var AvgTotal = calcTotalGrade(professor.Reviews);
            gradesDict.Add(
                "Ukupna ocjena",
                new GradeInfo{Grade = AvgTotal}
            );
            var model = new ProfessorDetailViewModel
            {
                ID = professor.ID,
                FirstName = professor.FirstName,
                LastName = professor.LastName,
                Reviews = professor.Reviews.OrderBy(r => r.Points).Take(2).ToList(),
                Subjects = subjects.ToList(),
                Biography = professor.Biography,
                Validated = professor.Validated,
                AvgAccessibility = AvgAccessibility,
                AvgInteractivity = AvgInteractivity,
                AvgMentoring = AvgMentoring,
                AvgTotal = AvgTotal,
                AvgQuality = AvgQuality,
                Faculties = faculties.ToList(),
                Universities = faculties.Select(f => f.University).Distinct().ToList(),
                Interests = subjects.SelectMany(s => s.SubjectTagSet).Select(s => s.SubjectTag).Distinct().ToList(),
                Grades = gradesDict
            };

            return View(model);
        }

        private double calcQuality(ICollection<ProfessorReview> reviews)
        {
            return reviews.Count() != 0 ? Math.Round(reviews.Select(r => r.QualityGrade).Average(), 2) : 0;
        }

        private double calcTotalGrade(ICollection<ProfessorReview> reviews)
        {
            return reviews.Count() != 0 ? Math.Round((double)reviews.Select(r => r.TotalGrade).Average(), 2) : 0;
        }

        private double calcMentoring(ICollection<ProfessorReview> reviews)
        {
            double result;
            if(reviews.Count() != 0)
            {
                var query = reviews.Where(r => r.MentorGrade.HasValue).Select(r => r.MentorGrade);
                result = query.Count() != 0 ? (double)query.Average() : 0;
            }
            else
            {
                result = 0;
            }
            return Math.Round(result, 2);
        }

        private double calcInteractivity(ICollection<ProfessorReview> reviews)
        {
            return reviews.Count() != 0 ? Math.Round(reviews.Select(r => r.InteractionGrade).Average(), 2) : 0;
        }

        private double calcAccessibility(ICollection<ProfessorReview> reviews)
        {
            return reviews.Count() != 0 ? Math.Round(reviews.Select(r => r.HelpfulnessGrade).Average(), 2) : 0;
        }

        // GET: Professor/Create
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> Create()
        {
            var currUser = await _userManager.GetUserAsync(User);
            bool isUser;
            if(await _userManager.IsInRoleAsync(currUser, "Administrator") || await _userManager.IsInRoleAsync(currUser, "Moderator"))
            {
                isUser = false;
            }
            else
            {
                isUser = true;
            }

            ViewData["IsUser"] = isUser;
            ViewData["SubjectID"] = new SelectList(_context.Subject, "ID", "Name");
            return View();
        }

        // POST: Professor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Korisnik")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Biography")] Professor professor, ICollection<int> SubjectIDs, bool isUser)
        {
            professor.Validated = !isUser;

            List<ProfessorSubjectSet> temp = new List<ProfessorSubjectSet>();
            var subjects = _context.Subject.AsNoTracking().Select(s => s.ID).ToList();

            foreach(var id in SubjectIDs)
            {
                if(subjects.Any(s => s == id))
                {
                    temp.Add(new ProfessorSubjectSet { ProfessorID = professor.ID, SubjectID = id});
                }
            }

            professor.Subjects = temp;

            if (ModelState.IsValid)
            {
                _context.Professor.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
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

        public IActionResult Validate(int id)
        {
            var professor = _context.Professor.SingleOrDefault(p => p.ID == id);
            if (professor == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    professor.Validated = !professor.Validated;
                    _context.Update(professor);
                    _context.SaveChanges();
                    var result = new
                    {
                        message = $"Profesor {professor.FullName} potvrðen.",
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
    }
}
