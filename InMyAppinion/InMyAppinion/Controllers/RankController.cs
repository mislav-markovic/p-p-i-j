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
    }
}