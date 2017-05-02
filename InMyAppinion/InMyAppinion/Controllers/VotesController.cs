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
    public class VotesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VotesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> VoteReview(bool vote, int id, string type)
        {
            if(type.ToLower() == "professor")
            {
                int voteValue = 0;

                if (vote) voteValue = 1;
                else voteValue = -1;

                var voter = _userManager.GetUserId(User);
                var review = await _context.ProfessorReview.SingleOrDefaultAsync(r => r.ID == id);
                var hasVoted = await _context.VoteProfessorReview.SingleOrDefaultAsync(v => v.ProfessorReviewID == id && v.VoterID == voter);

                if (review == null)
                {
                    return NotFound();
                }

                if (hasVoted != null)
                {
                    if(hasVoted.Vote != vote)
                    {
                        review.Points += 2 * voteValue;
                        hasVoted.Vote = vote;

                        try
                        {
                            _context.VoteProfessorReview.Update(hasVoted);
                            _context.ProfessorReview.Update(review);
                            _context.SaveChanges();

                            var result = new
                            {
                                successful = true,
                                message = "Glasanje uspješno obavljeno",
                                points = review.Points
                            };
                            return Json(result);
                        }
                        catch (Exception e)
                        {
                            var error = new
                            {
                                successful = false,
                                message = "Glasanje nije uspjelo",
                            };
                            return Json(error);
                        }
                    }
                    else
                    {
                        var result = new
                        {
                            successful = false,
                            message = "Isti glas"
                        };
                        return Json(result);
                    }

                }

                var voteTable = new VoteProfessorReview
                {
                    VoterID = voter,
                    Vote = vote,
                    ProfessorReviewID = id
                };

                review.Points += voteValue;

                try
                {
                    _context.VoteProfessorReview.Add(voteTable);
                    _context.ProfessorReview.Update(review);
                    _context.SaveChanges();

                    var result = new
                    {
                        successful = true,
                        message = "Glasanje uspješno obavljeno",
                        points = review.Points
                    };
                    return Json(result);
                }
                catch (Exception e)
                {
                    var error = new
                    {
                        successful = false,
                        message = "Glasanje nije uspjelo",
                    };
                    return Json(error);
                }
            }


            return NotFound();
        }
    }
}