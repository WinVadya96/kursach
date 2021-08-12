using kursach.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kursach.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        
        //private readonly ApplicationDbContext dbContext;
        //public HomeController(ApplicationDbContext dbContext)
        //{
        //    this.dbContext = dbContext;
        //}

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize]
        public ActionResult Control()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult GetUsers()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                users = db.Users.ToList();
            }
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(ApplicationUser user)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        // Позволяет получить роли пользователя
        public ActionResult GetMyRole()
        {
            IList<string> roles = new List<string> { "Роль не определена"};
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
        }

        //[Authorize]
        //public ActionResult ViewUsers()
        //{
        //    var users = dbContext.Set<ApplicationUser>()
        //        .ToList()
        //        .Select(u => new UsersModel
        //        {
        //            Id = u.Id,
        //            Name = u.UserName,
        //            Email = u.Email,
        //            IsAdminIn = u.IsAdminIn
        //        });

        //    return View(users);
        //}
    }
}