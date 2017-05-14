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
            int voteValue = 0;

            if (vote) voteValue = 1;
            else voteValue = -1;

            var voter = _userManager.GetUserId(User);

            if (type.ToLower() == "professor")
            {
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

                            await AddPoints(review.AuthorID, 2 * voteValue);

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

                    await AddPoints(review.AuthorID, voteValue);

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
            else if(type.ToLower() == "subject")
            {
                var review = await _context.SubjectReview.SingleOrDefaultAsync(r => r.ID == id);
                var hasVoted = await _context.VoteSubjectReview.SingleOrDefaultAsync(v => v.SubjectReviewID == id && v.VoterID == voter);

                if (review == null)
                {
                    return NotFound();
                }

                if (hasVoted != null)
                {
                    if (hasVoted.Vote != vote)
                    {
                        review.Points += 2 * voteValue;
                        hasVoted.Vote = vote;

                        try
                        {
                            _context.VoteSubjectReview.Update(hasVoted);
                            _context.SubjectReview.Update(review);
                            _context.SaveChanges();

                            await AddPoints(review.AuthorID, 2 * voteValue);

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

                var voteTable = new VoteSubjectReview
                {
                    VoterID = voter,
                    Vote = vote,
                    SubjectReviewID = id
                };

                review.Points += voteValue;

                try
                {
                    _context.VoteSubjectReview.Add(voteTable);
                    _context.SubjectReview.Update(review);
                    _context.SaveChanges();

                    await AddPoints(review.AuthorID, voteValue);

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
                var error = new
                {
                    successful = false,
                    message = "Pogrešan tip glasa"
                };
                return Json(error);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> VoteComment(bool vote, int id)
        {
            int voteValue = 0;

            if (vote) voteValue = 1;
            else voteValue = -1;

            var voter = _userManager.GetUserId(User);
            var comment = await _context.Comment.SingleOrDefaultAsync(r => r.ID == id);
            var hasVoted = await _context.VoteComment.SingleOrDefaultAsync(v => v.CommentID == id && v.VoterID == voter);

            if (comment == null)
            {
                return NotFound();
            }

            if (hasVoted != null)
            {
                if (hasVoted.Vote != vote)
                {
                    comment.Points += 2 * voteValue;
                    hasVoted.Vote = vote;

                    try
                    {
                        _context.VoteComment.Update(hasVoted);
                        _context.Comment.Update(comment);
                        _context.SaveChanges();

                        await AddPoints(comment.AuthorID, 2 * voteValue);

                        var result = new
                        {
                            successful = true,
                            message = "Glasanje na komentaru uspješno obavljeno",
                            points = comment.Points
                        };
                        return Json(result);
                    }
                    catch (Exception e)
                    {
                        var error = new
                        {
                            successful = false,
                            message = "Glasanje na komentaru nije uspjelo",
                        };
                        return Json(error);
                    }
                }
                else
                {
                    var result = new
                    {
                        successful = false,
                        message = "Isti glas na komentaru"
                    };
                    return Json(result);
                }

            }

            var voteTable = new VoteComment
            {
                VoterID = voter,
                Vote = vote,
                CommentID = id
            };

            comment.Points += voteValue;

            try
            {
                _context.VoteComment.Add(voteTable);
                _context.Comment.Update(comment);
                _context.SaveChanges();

                await AddPoints(comment.AuthorID, voteValue);

                var result = new
                {
                    successful = true,
                    message = "Glasanje na komentaru uspješno obavljeno",
                    points = comment.Points
                };
                return Json(result);
            }
            catch (Exception e)
            {
                var error = new
                {
                    successful = false,
                    message = "Glasanje na komentaru nije uspjelo",
                };
                return Json(error);
            }
        }

        public async Task<IActionResult> AddPoints(string userId, int vote)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.Points += vote;
            await _userManager.UpdateAsync(user);

            return null;
        }

        public async Task<IActionResult> UpdateAllPoints()
        {
            var users = _context.User.ToList();
            foreach(var user in users)
            {
                user.Points = 0;
                _context.User.Update(user);
                _context.SaveChanges();
            }

            var profReviews = _context.ProfessorReview.ToList();
            foreach(var rev in profReviews)
            {
                var user = _context.User.SingleOrDefault(u => u.Id == rev.AuthorID);
                user.Points += rev.Points;
                _context.User.Update(user);
                _context.SaveChanges();
            }

            var subjectReviews = _context.SubjectReview.ToList();
            foreach (var rev in subjectReviews)
            {
                var user = _context.User.SingleOrDefault(u => u.Id == rev.AuthorID);
                user.Points += rev.Points;
                _context.User.Update(user);
                _context.SaveChanges();
            }

            var comments = _context.Comment.ToList();
            foreach(var com in comments)
            {
                var user = _context.User.SingleOrDefault(u => u.Id == com.AuthorID);
                user.Points += com.Points;
                _context.User.Update(user);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home", "");
        }
    }
}