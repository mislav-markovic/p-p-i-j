using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InMyAppinion.Data;
using InMyAppinion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InMyAppinion.Controllers
{
    [Authorize(Policy = "CanModerate")]
    public class CPController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CPController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

        public async Task<IActionResult> BanUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if(user == null)
            {
                return NotFound($"Ne postoji korisnik s ID-om: {id}!");
            }
            if(await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(User.Identity.Name), "Moderator") &&
               (await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(id), "Moderator") ||
                await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(id), "Administrator")))
            {
                var result = new
                {
                    message = "Ne možete banati korisnika veæeg ili istog ranga!",
                    success = false
                };
                return Json(result);
            }
            try
            {
                var msg = "";
                if (user.IsBanned)
                {
                    msg = $"Korisnik {user.UserName} izgubio bananu!";
                    await _userManager.SetLockoutEnabledAsync(user, false);
                    user.LockoutEnd = null;
                }
                else{
                    msg = $"Korisnik {user.UserName} dobio bananu!";
                    await _userManager.SetLockoutEnabledAsync(user, true);
                    user.LockoutEnd = new DateTimeOffset(DateTime.Now).AddDays(7);
                }
                user.IsBanned = !user.IsBanned;
                _context.Update(user);
                _context.SaveChanges();
                var result = new
                {
                    message = msg,
                    success = true
                };
                return Json(result);
            }
            catch (Exception exc)
            {
                var result = new
                {
                    message = $"Nije moguæe banati usera {user.UserName}!",
                    success = true
                };
                return Json(result);
            }
        }

        public IActionResult SetSubjectTags()
        {
            ViewData["Subjects"] = new SelectList(_context.Subject, "ID", "Name");
            ViewData["Tags"] = new SelectList(_context.SubjectTag, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetSubjectTags(int id, string[] tags)
        {
            var subject = _context.Subject.SingleOrDefault(s => s.ID == id);
            if(subject == null)
            {
                return NotFound();
            }

            foreach(var tagName in tags)
            {
                var tag = _context.SubjectTag.SingleOrDefault(t => t.Name == tagName);
                if(tag == null)
                {
                    tag = new SubjectTag { Name = tagName };
                    _context.SubjectTag.Add(tag);
                    await _context.SaveChangesAsync();
                }
                if (_context.SubjectTagSet.SingleOrDefault(st => st.SubjectID == id && st.SubjectTagID == tag.ID) == null)
                {
                    _context.SubjectTagSet.Add(
                        new SubjectTagSet
                        {
                            SubjectID = id,
                            SubjectTagID = tag.ID,
                        }
                    );
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}