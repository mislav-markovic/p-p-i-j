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

                    var model = new SubjectSearchViewModel();
                try
                {
                    model.tags = _context.SubjectTag.Where(o => o.Name.ToLower().Contains(query.ToLower())).ToList();
                    var tmp = new List<Models.Subject>();
                    var subjects = _context.Subject.ToList();
                    model.query = query;
                    foreach (var tag in model.tags)
                    {
                        model.subtagset = _context.SubjectTagSet.Where(o => o.SubjectTagID == tag.ID).ToList();

                        
                        foreach (var sts in model.subtagset)
                        {
                            tmp.Add(sts.Subject);
                        }
                    }
                    //var tmp = _context.Subject.FirstOrDefault();

                    
                    model.subjects = tmp;
                }
                //model.subjects = _context.Subject.ToList();
                catch {
                    model.query = query;
                }
                return View(model);

            }
            var model2 = new SubjectSearchViewModel();
            model2.subjects = _context.Subject.ToList();
            //model2.subtagset = _context.SubjectTagSet.ToList();
            model2.query = query;
            return View(model2);
        }
    }
}