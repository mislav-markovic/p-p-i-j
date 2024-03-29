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
using Newtonsoft.Json;
using InMyAppinion.Models.ProfessorViewModels;
using InMyAppinion.Models.SubjectViewModels;
using InMyAppinion.ViewModels;

namespace InMyAppinion.Controllers
{
    public class RankController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RankController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GradeByYear(int id, string type)
        {
            dynamic source;
            try
            {
                if (type.ToLower() == "professor")
                {
                    source = _context.ProfessorReview.Where(r => r.ProfessorID == id).ToList();
                }
                else if (type.ToLower() == "subject")
                {
                    source = _context.SubjectReview.Where(r => r.SubjectID == id).ToList();
                }
                else
                {
                    //Vratiti ispravni JSON ovdje
                    return NotFound();
                }

                Dictionary<int, int> counterDict = new Dictionary<int, int>();
                Dictionary<int, decimal> sumDict = new Dictionary<int, decimal>();

                foreach (var item in source)
                {
                    DateTime year = item.Timestamp;
                    decimal grade = item.TotalGrade;

                    if (counterDict.ContainsKey(year.Year))
                    {
                        counterDict[year.Year] += 1;
                    }
                    else
                    {
                        counterDict.Add(year.Year, 1);
                    }

                    if (sumDict.ContainsKey(year.Year))
                    {
                        sumDict[year.Year] += grade;
                    }
                    else
                    {
                        sumDict.Add(year.Year, grade);
                    }
                }

                var keys = sumDict.Keys.ToList();
                foreach (var year in keys)
                {
                    sumDict[year] = sumDict[year] / counterDict[year];
                }

                string json = JsonConvert.SerializeObject(sumDict);

                return Json(json);
            }catch(Exception e)
            {
                return Json(e.Message);
            }
        }

        public IActionResult ProfessorRankings(int sort = 5, int tag = -1)
        {
            var query = _context.Professor
                .Include(p => p.Reviews)
                .Include(s => s.Subjects)
                    .ThenInclude(s => s.Subject)
                        .ThenInclude(s => s.SubjectTagSet)
                            .ThenInclude(s => s.SubjectTag)
                .AsNoTracking().Where(p => p.Reviews.Count > 0 && p.Validated).ToList();
            List<ProfessorDetailViewModel> profList = new List<ProfessorDetailViewModel>();

            foreach(var professor in query)
            {
                var subjects = professor.Subjects.Select(p => p.Subject);
                var model = new ProfessorDetailViewModel
                {
                    ID = professor.ID,
                    FirstName = professor.FirstName,
                    LastName = professor.LastName,
                    Reviews = professor.Reviews,
                    Subjects = subjects.ToList(),
                    Biography = professor.Biography,
                    Validated = professor.Validated,
                    Interests = subjects.SelectMany(s => s.SubjectTagSet).Select(s => s.SubjectTag).Distinct().ToList(),
                    AvgAccessibility = calcAccessibility(professor.Reviews),
                    AvgInteractivity = calcInteractivity(professor.Reviews),
                    AvgMentoring = calcMentoring(professor.Reviews),
                    AvgTotal = calcTotalGrade(professor.Reviews),
                    AvgQuality = calcQuality(professor.Reviews)
                };

                profList.Add(model);
            }

            if(tag != -1)
            {
                profList = profList.Where(p => p.Interests.Any(i => i.ID == tag)).ToList();
            }

            System.Linq.Expressions.Expression<Func<ProfessorDetailViewModel, object>> orderSelector = null;
            switch (sort)
            {
                case 1:
                    orderSelector = d => d.AvgAccessibility;
                    break;
                case 2:
                    orderSelector = d => d.AvgInteractivity;
                    break;
                case 3:
                    orderSelector = d => d.AvgMentoring;
                    break;
                case 4:
                    orderSelector = d => d.AvgQuality;
                    break;
                case 5:
                    orderSelector = d => d.AvgTotal;
                    break;
                default:
                    orderSelector = d => d.AvgTotal;
                    break;
            }

            var q = profList.AsQueryable();
            var sorted = q.OrderByDescending(orderSelector).Take(10).ToList();

            var selectList = new SelectList(_context.SubjectTag, "ID", "Name");
            if (tag != -1)
            {
                var selected = selectList.Where(i => i.Value.Equals(tag.ToString())).First();
                selected.Selected = true;
            }

            ViewData["SubjectTag"] = selectList;
            return View(new ProfessorRankingViewModel { Professors = sorted, sort = sort, tag = tag});
        }

        public IActionResult SubjectRankings(int sort = 4, int tag = -1)
        {
            var query = _context.Subject
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
                .AsNoTracking().Where(s => s.Reviews.Count > 0 && s.Validated).ToList();

            List<SubjectDetailViewModel> subjectList = new List<SubjectDetailViewModel>();

            foreach (var subject in query)
            {
                var tagList = subject.SubjectTagSet.Select(set => set.SubjectTagID).ToList();

                if(tag != -1 && !tagList.Contains(tag))
                {
                    continue;
                }

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
                    AvgDifficultyGrade = calcDifficulty(subject.Reviews),
                    AvgInterestGrade = calcInterest(subject.Reviews),
                    AvgUsefulnessGrade = calcUsefulness(subject.Reviews),
                    AvgTotal = calcTotalGrade(subject.Reviews),
                    Grades = new Dictionary<string, GradeInfo>()
                };

                subjectList.Add(model);
            }

            System.Linq.Expressions.Expression<Func<SubjectDetailViewModel, object>> orderSelector = null;
            switch (sort)
            {
                case 1:
                    orderSelector = d => d.AvgDifficultyGrade;
                    break;
                case 2:
                    orderSelector = d => d.AvgInterestGrade;
                    break;
                case 3:
                    orderSelector = d => d.AvgUsefulnessGrade;
                    break;
                case 4:
                    orderSelector = d => d.AvgTotal;
                    break;
                default:
                    orderSelector = d => d.AvgTotal;
                    break;
            }

            var q = subjectList.AsQueryable();
            var sorted = q.OrderByDescending(orderSelector).Take(10).ToList();

            var selectList = new SelectList(_context.SubjectTag, "ID", "Name");
            if(tag != -1)
            {
                var selected = selectList.Where(i => i.Value.Equals(tag.ToString())).First();
                selected.Selected = true;
            }

            ViewData["SubjectTag"] = selectList;
            return View(new SubjectRankingViewModel { Subjects = sorted, tag = tag, sort = sort });
        }

#region gradeHelpers

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
            if (reviews.Count() != 0)
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

#endregion gradeHelpers

    }
}