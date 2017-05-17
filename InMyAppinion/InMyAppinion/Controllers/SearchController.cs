using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.ViewModels;
using Microsoft.AspNetCore.Mvc;
using InMyAppinion.Data;
using Microsoft.EntityFrameworkCore;
using InMyAppinion.ViewModels.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InMyAppinion.Controllers
{
    public class SearchController : Controller
    {

        private readonly ApplicationDbContext _context;

        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Search(string query)
        {
            if (query != null && query!="") {

                    var model = new SearchViewModel();
                    model.subservmod = new SubjectSearchViewModel();
                    model.profservmod = new ProfessorSearchViewModel();
                try
                {

                    // subject part
                    model.subservmod.tags = _context.SubjectTag.Where(o => o.Name.ToLower().Contains(query.ToLower())).ToList();
                    var tmp = new HashSet<Models.Subject>();
                    var subjects = _context.Subject.Include(s => s.Faculty).ToList();
                    model.query = query;
                    foreach (var tag in model.subservmod.tags)
                    {
                        model.subservmod.subtagset = _context.SubjectTagSet.Include(s=>s.Subject).ThenInclude(s=>s.Faculty).Where(o => o.SubjectTagID == tag.ID).ToList();

                        
                        foreach (var sts in model.subservmod.subtagset)
                        {
                            if (sts.Subject.Validated) { 
                                tmp.Add(sts.Subject);
                            }

                        }
                    }
                    //var tmp = _context.Subject.FirstOrDefault();
                    subjects = subjects.Where(s => s.Name.ToLower().Contains(query.ToLower()) && s.Validated).ToList();
                    foreach (var subject in subjects)
                    {
                        if (!tmp.Contains(subject))
                        {
                            tmp.Add(subject);
                        }
                    }
                    model.subservmod.subjects = tmp.OrderBy(s => s.Name);


                    // professor part
                    var profs = _context.Professor.Where(o => o.Validated && o.FullName.ToLower().Contains(query.ToLower())).ToList();
                    var x = _context.Subject.Where(s => tmp.Contains(s)).SelectMany(s => s.Professors).Select(s => s.Professor).Where(p => p.Validated).ToList();
                    foreach (var y in x)
                    {
                        if (!profs.Contains(y))
                        {
                            profs.Add(y);
                        }
                    }

                    model.profservmod.professors = profs.OrderBy(p => p.FullName);
                }
                //model.subjects = _context.Subject.ToList();
                catch {
                    model.query = query;
                }
                return View(model);

            }
            var model2 = new SearchViewModel();
            model2.subservmod = new SubjectSearchViewModel();
            model2.profservmod = new ProfessorSearchViewModel();
            model2.profservmod.professors = _context.Professor.Where(o=>o.Validated).ToList();
            model2.subservmod.subjects = _context.Subject.Include(s=>s.Faculty).Where(o=>o.Validated).ToList();
            //model2.subtagset = _context.SubjectTagSet.ToList();
            model2.query = query;
            return View(model2);
        }

        public IActionResult Advanced(string[] array) {
            string filter = string.Join("|", array);
            var sfilter = SearchFilter.FromString(filter);
            if (!sfilter.IsEmpty())
            {
                sfilter.Initialize(_context);
                sfilter.ApplyFilter();
                return View(sfilter);
            }
            else {
                return RedirectToAction("Search");
            }
        }

        public IActionResult AdvancedSearchForm()
        {
            ViewData["cities"] = new SelectList(_context.City.OrderBy(c => c.Name).ToList(), "Name", "Name");
            ViewData["faculties"] = new SelectList(_context.Faculty.OrderBy(f => f.Name).ToList(), "Name", "Name");
            ViewData["professors"] = new SelectList(_context.Professor.Where(p => p.Validated).OrderBy(p => p.FullName).ToList(), "FullName", "FullName");
            return View();
        }

    }
}