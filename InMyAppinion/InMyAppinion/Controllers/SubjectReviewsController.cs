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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace InMyAppinion.Controllers
{
    public class SubjectReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubjectReviewsController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: SubjectReviews
        public async Task<IActionResult> Index(string username)
        {
            var applicationDbContext = _context.SubjectReview.Include(s => s.Author).Include(s => s.Subject);
            if (username == null)
            {
                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                return View(await applicationDbContext.Where(s => s.Author.UserName == username).ToListAsync());
            }
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
                .Include(s => s.SubjectReviewTagSet).ThenInclude(s => s.SubjectReviewTag)
                .Include(s => s.Comments).ThenInclude(s => s.Author)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (subjectReview == null)
            {
                return NotFound();
            }

            if (_signInManager.IsSignedIn(User))
            {
                var voted = await _context.VoteSubjectReview
                            .Where(v => v.SubjectReview.ID == subjectReview.ID && v.Voter.UserName == User.Identity.Name)
                            .SingleOrDefaultAsync();
                if (voted != null)
                {
                    ViewData["voted"] = voted.Vote.ToString();
                }

                string userId = _userManager.GetUserId(User);
                var list = _context.VoteComment.Where(v => v.VoterID == userId);

                foreach (var comm in subjectReview.Comments)
                {
                    if (list.Any(k => k.CommentID == comm.ID))
                    {
                        ViewData[comm.ID.ToString()] = list.First(k => k.CommentID == comm.ID).Vote.ToString();
                    }
                    else
                    {
                        ViewData[comm.ID.ToString()] = "";
                    }
                }

            }

            return View(subjectReview);
        }

        // GET: SubjectReviews/Create
        public IActionResult Create(int? Id)
        {
            if (!Id.HasValue)
            {
                return NotFound();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);


            ViewData["AuthorID"] = userId;
            ViewData["SubjectID"] = Id;
            ViewData["SubjectName"] = _context.Subject.Where(s => s.ID == Id).Select(s=>s.Name).First();
            ViewData["SubjectTags"] = new SelectList(_context.SubjectReviewTag, "ID", "Name");
            return View();
        }

        // POST: SubjectReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Text,UsefulnessGrade,InterestGrade,DifficultyGrade,Points,AuthorID,SubjectID")] SubjectReview subjectReview, List<int> tags)
        {
            subjectReview.TotalGrade = calculateTotalGrade(subjectReview);
            subjectReview.Timestamp = DateTime.Now;
            List<SubjectReviewTagSet> tagSet = new List<SubjectReviewTagSet>();

            foreach(var tag in tags)
            {
                SubjectReviewTagSet temp = new SubjectReviewTagSet();
                temp.SubjectReviewID = subjectReview.ID;
                temp.SubjectReviewTagID = tag;
                tagSet.Add(temp);
            }
            subjectReview.SubjectReviewTagSet = tagSet;

            if (ModelState.IsValid)
            {
                _context.Add(subjectReview);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AuthorID"] = subjectReview.AuthorID;
            ViewData["SubjectID"] = subjectReview.SubjectID;
            ViewData["SubjectName"] = _context.Subject.Where(s => s.ID == subjectReview.SubjectID).Select(s=>s.Name).First();
            ViewData["SubjectTags"] = new SelectList(_context.SubjectReviewTag, "ID", "Name");
            return View();
        }

        private decimal calculateTotalGrade(SubjectReview sr)
        {
            int sum = 0;
            decimal result = 0;

            sum += sr.UsefulnessGrade;
            sum += sr.InterestGrade;
            sum += sr.DifficultyGrade;

            result = sum / (decimal) 3;
            return result;

        }

        // GET: SubjectReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subjectReview = await _context.SubjectReview.Include(s=>s.Subject).SingleOrDefaultAsync(m => m.ID == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Text,UsefulnessGrade,InterestGrade,DifficultyGrade,TotalGrade,Points,Timestamp,AuthorID,SubjectID")] SubjectReview subjectReview)
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
            try
            {
                var comments = _context.Comment.Where(c => c.SubjectReviewID == id);
                ApplicationUser user = null;
                foreach (var comment in comments)
                {
                    user = await _context.User.SingleOrDefaultAsync(u => u.Id == comment.AuthorID);
                    user.Points -= comment.Points;
                    _context.User.Update(user);
                }
                _context.SubjectReview.Remove(subjectReview);

                user = await _context.User.SingleOrDefaultAsync(u => u.Id == subjectReview.AuthorID);
                user.Points -= subjectReview.Points;
                _context.User.Update(user);

                await _context.SaveChangesAsync();

                TempData[Constants.Message] = $"Recenzija uspješno obrisana.";
                TempData[Constants.ErrorOccurred] = false;
            }
            catch (Exception exc)
            {
                ModelState.AddModelError(string.Empty, exc.ToString());
                TempData[Constants.Message] = "Pogreška u brisanju recenzije";
                TempData[Constants.ErrorOccurred] = true;
                return RedirectToAction("Details", new { id = id });
            }
            return RedirectToAction("Index");
        }

        private bool SubjectReviewExists(int id)
        {
            return _context.SubjectReview.Any(e => e.ID == id);
        }
    }
}
