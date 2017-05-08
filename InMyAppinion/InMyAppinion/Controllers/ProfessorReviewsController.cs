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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProfessorReview
                .Include(p => p.Author)
                .Include(p => p.Professor);
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
        [Authorize(Roles = "Administrator,Korisnik")]
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
        public async Task<IActionResult> Create([Bind("Text,QualityGrade,InteractionGrade,HelpfulnessGrade,MentorGrade,Points,AuthorID,ProfessorID")] ProfessorReview professorReview, ICollection<int> tags)
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
