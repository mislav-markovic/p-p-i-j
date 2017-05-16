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
    public class ProfessorReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfessorReviewsController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: ProfessorReviews
        public async Task<IActionResult> Index(string username, int id = -1)
        {
            var applicationDbContext = _context.ProfessorReview
                .Include(p => p.Author)
                .Include(p => p.Professor);
            if (username == null)
            {
                if(id != -1)
                {
                    return View(await _context.ProfessorReview.Where(r => r.ProfessorID == id).ToListAsync());
                }
                else
                { 
                    return View(await applicationDbContext.ToListAsync());
                }
            }
            else
            {
                return View(await applicationDbContext.Where(s => s.Author.UserName == username).ToListAsync());
            }
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
                .Include(p => p.ProfessorReviewTagSet).ThenInclude(p => p.ProfessorReviewTag)
                .Include(p => p.Comments).ThenInclude(p => p.Author)
                .SingleOrDefaultAsync(m => m.ID == id);

            if (professorReview == null)
            {
                return NotFound();
            }

            if(_signInManager.IsSignedIn(User)){
                var voted = await _context.VoteProfessorReview
                            .Where(v => v.ProfessorReview.ID == professorReview.ID && v.Voter.UserName == User.Identity.Name)
                            .SingleOrDefaultAsync();
                if(voted != null){
                    ViewData["voted"] = voted.Vote.ToString();
                }

                string userId = _userManager.GetUserId(User);
                var list = _context.VoteComment.Where(v => v.VoterID == userId);

                foreach(var comm in professorReview.Comments)
                {
                    if(list.Any(k => k.CommentID == comm.ID))
                    {
                        ViewData[comm.ID.ToString()] = list.First(k => k.CommentID == comm.ID).Vote.ToString();
                    }
                    else
                    {
                        ViewData[comm.ID.ToString()] = "";
                    }
                }

            }

            return View(professorReview);
        }


        // GET: ProfessorReviews/Create
        [Authorize(Roles = "Korisnik")]
        public IActionResult Create(int? Id)
        {
            if (!Id.HasValue)
            {
                return NotFound();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["AuthorID"] = userId;
            ViewData["ProfessorID"] = Id;
            ViewData["ProfessorTags"] = new SelectList(_context.ProfessorReviewTag, "ID", "Name");

            return View();
        }

        // POST: ProfessorReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Text,QualityGrade,InteractionGrade,HelpfulnessGrade,MentorGrade,Points,AuthorID,ProfessorID")] ProfessorReview professorReview, ICollection<int> tags)
        {
            if (professorReview.MentorGrade == 0) professorReview.MentorGrade = null;
            professorReview.TotalGrade = calculateTotalGrade(professorReview);
            professorReview.Timestamp = DateTime.Now;
            List<ProfessorReviewTagSet> tagSet = new List<ProfessorReviewTagSet>();

            foreach(var tagId in tags)
            {
                var temp = new ProfessorReviewTagSet();
                temp.ProfessorReviewID = professorReview.ID;
                temp.ProfessorReviewTagID = tagId;
                tagSet.Add(temp);
            }

            professorReview.ProfessorReviewTagSet = tagSet;

            if (ModelState.IsValid)
            {
                _context.Add(professorReview);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["AuthorID"] = professorReview.AuthorID;
            ViewData["ProfessorID"] = professorReview.ProfessorID;
            ViewData["ProfessorTags"] = new SelectList(_context.ProfessorReviewTag, "ID", "Name");
            return View();
        }

        private decimal calculateTotalGrade(ProfessorReview pr)
        {
            int sum = 0;
            decimal result = 0;

                sum += pr.HelpfulnessGrade; 
                sum += pr.InteractionGrade;
                sum += pr.QualityGrade;

            if (pr.MentorGrade.HasValue)
            {
                sum += pr.MentorGrade.Value;
                result = sum / (decimal) 4;    
            }
            else
            {
                result = sum / (decimal)3;
            }

            return result;

        }

        // GET: ProfessorReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorReview = await _context.ProfessorReview.Include(p=>p.Professor).Include(a=>a.Author).SingleOrDefaultAsync(m => m.ID == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Text,QualityGrade,InteractionGrade,HelpfulnessGrade,MentorGrade,TotalGrade,Points,Timestamp,AuthorID,ProfessorID")] ProfessorReview professorReview)
        {
            if (id != professorReview.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    professorReview.TotalGrade = calculateTotalGrade(professorReview);
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
            try
            {
                var comments = _context.Comment.Where(c => c.ProfessorReviewID == id);
                ApplicationUser user = null;
                foreach(var comment in comments)
                {
                    user = await _context.User.SingleOrDefaultAsync(u => u.Id == comment.AuthorID);
                    user.Points -= comment.Points;
                    _context.User.Update(user);
                }
                _context.Comment.RemoveRange(comments);
                _context.ProfessorReview.Remove(professorReview);

                user = await _context.User.SingleOrDefaultAsync(u => u.Id == professorReview.AuthorID);
                user.Points -= professorReview.Points;
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

        private bool ProfessorReviewExists(int id)
        {
            return _context.ProfessorReview.Any(e => e.ID == id);
        }
    }
}
