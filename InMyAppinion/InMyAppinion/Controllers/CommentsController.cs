using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InMyAppinion.Data;
using Microsoft.EntityFrameworkCore;
using InMyAppinion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace InMyAppinion.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(string username)
        {
            var comments = _context.Comment
                           .Include(c => c.Author)
                           .Include(c => c.ProfessorReview)
                           .Include(c => c.SubjectReview);

            if (username == null) {
                return View(comments.ToList());
            }

            return View(comments.Where(c => c.Author.UserName == username));
        }

        [HttpPost]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> Create(int id, string type, string text)
        {
            Comment comment = new Comment
            {
                AuthorID = _userManager.GetUserId(User),
                Author = await _userManager.GetUserAsync(User),
                Timestamp = DateTime.Now,
                Text = text
            };

            dynamic review = null;
            if (type.ToLower() == "subject")
            {
                review = await _context.SubjectReview.SingleOrDefaultAsync(r => r.ID == id);
                comment.SubjectReviewID = id;
            } 
            else if(type.ToLower() == "professor")
            {
                review = await _context.ProfessorReview.SingleOrDefaultAsync(r => r.ID == id);
                comment.ProfessorReviewID = id;
            }
            else
            {
                var result = new
                {
                    message = "Pogreška u dodavanju komentara!",
                    success = false
                };
                return Json(result);
            }

            if (review == null)
            {
                return NotFound();
            }

            try
            {
                _context.Comment.Add(comment);
                await _context.SaveChangesAsync();

                var result = new
                {
                    message = "Komentar uspješno dodan.",
                    success = true,
                    commentId = comment.ID
                };
                return Json(result);
            }
            catch (Exception exc)
            {
                var result = new
                {
                    message = "Pogreška u dodavanju komentara!",
                    success = false
                };
                return Json(result);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.SingleOrDefaultAsync(c => c.ID == id);
            if(comment == null)
            {
                return NotFound();
            }

            try
            {
                _context.Comment.Remove(comment);

                var user = await _context.User.SingleOrDefaultAsync(u => u.Id == comment.AuthorID);
                user.Points -= comment.Points;
                _context.User.Update(user);

                await _context.SaveChangesAsync();

                var result = new
                {
                    message = "Komentar uspješno obrisan.",
                    success = true,
                };
                return Json(result);
            }
            catch(Exception exc)
            {
                var result = new
                {
                    message = "Pogreška u brisanju komentara!",
                    success = false
                };
                return Json(result);
            }
        }
    }
}