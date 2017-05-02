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
    }
}