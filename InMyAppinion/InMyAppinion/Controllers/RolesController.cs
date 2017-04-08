using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using InMyAppinion.Models;
using InMyAppinion.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace InMyAppinion.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;

        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            List<RoleListViewModel> model = new List<RoleListViewModel>();
            model = roleManager.Roles.Select(r => new RoleListViewModel
            {
                RoleName = r.Name,
                Description = r.Description
            }).ToList();

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, string description)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole { Name = name, Description = description };
                await roleManager.CreateAsync(role);

                return RedirectToAction("Index");
            }

            return View(name, description);
        }
    }
}