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
                    model.tag = _context.SubjectTag.Where(o => o.Name.ToLower()==query.ToLower()).FirstOrDefault();
                    model.subtagset = _context.SubjectTagSet.Where(o => o.SubjectTagID == model.tag.ID).ToList();
                    model.query = query;
                    var subjects = _context.Subject.ToList();
                    //var tmp = _context.Subject.FirstOrDefault();
                    var tmp = new List<Models.Subject>();
                    foreach (var sts in model.subtagset) {
                        tmp.Add(sts.Subject);
                    }
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