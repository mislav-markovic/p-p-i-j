using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMyAppinion.ViewModels;
using Microsoft.AspNetCore.Mvc;
using InMyAppinion.Data;

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
            if (query != null) {

                    var model = new SubjectSearchViewModel();
                    model.tag = _context.SubjectTag.Where(o => o.Name.ToLower() == query.ToLower()).FirstOrDefault();
                    model.subtagset = _context.SubjectTagSet.Where(o => o.SubjectTagID == model.tag.ID).ToList();
                //var tmp = _context.Subject.FirstOrDefault();
                //model.subjects = new List<Models.Subject>();
                /*foreach (var sts in model.subtagset)
                {
                    model.subjects.Append(sts.Subject);
                    model.subjects.Append(_context.Subject.Where(o => o.ShortName.ToLower() == "ppij").First());
                }*/
                    model.subjects = _context.Subject.ToList();

                    return View(model);

            }
            var model2 = new SubjectSearchViewModel();
            model2.subjects = _context.Subject.ToList();
            return View(model2);
        }
    }
}