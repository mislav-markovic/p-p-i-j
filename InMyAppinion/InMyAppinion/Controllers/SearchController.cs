using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.ViewModels;
using Microsoft.AspNetCore.Mvc;
using InMyAppinion.Data;
using Microsoft.EntityFrameworkCore;

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
                    var subjects = _context.Subject.ToList();
                    model.query = query;
                    foreach (var tag in model.subservmod.tags)
                    {
                        model.subservmod.subtagset = _context.SubjectTagSet.Where(o => o.SubjectTagID == tag.ID).ToList();

                        
                        foreach (var sts in model.subservmod.subtagset)
                        {
                            if (sts.Subject.Validated) { 
                                tmp.Add(sts.Subject);
                            }

                        }
                    }
                    //var tmp = _context.Subject.FirstOrDefault();  
                    model.subservmod.subjects = tmp;



                    // professor part
                    var profs = _context.Professor.Where(o => o.Validated && o.FullName.ToLower().Contains(query.ToLower())).ToList();
                    model.profservmod.professors = profs;
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
            model2.profservmod.professors = _context.Professor.ToList();
            model2.subservmod.subjects = _context.Subject.ToList();
            //model2.subtagset = _context.SubjectTagSet.ToList();
            model2.query = query;
            return View(model2);
        }
    }
}