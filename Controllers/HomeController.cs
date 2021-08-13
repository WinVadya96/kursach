using kursach.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Тут будут находится пользовательские коллекции.";

            return View();
        }


        //public ActionResult GetUsers()
        //{
        //    List<ApplicationUser> users = new List<ApplicationUser>();
        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        users = db.Users.ToList();
        //    }
        //    return View(users);
        //}

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetUsers()
        {
            IList<ApplicationUser> users = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                users = await db.Users.ToListAsync();
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

        [HttpGet]
        public ActionResult Delete(string id)
        {            
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser us = db.Users.Find(id);
                if (us == null)
                {
                    return HttpNotFound();
                }
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser us = db.Users.Find(id);
                if (us == null)
                {
                    return HttpNotFound();
                }
                db.Users.Remove(us);
                db.SaveChanges();
                return RedirectToAction("GetUsers");
            }
        }


        public ActionResult Blocked(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var z = user.IsBlocked.ToString();
                user.IsBlocked = z == "False";
                db.SaveChanges();
                return RedirectToAction("GetUsers");
            }
        }


        // Позволяет получить роли пользователя
        [Authorize]
        public ActionResult GetMyRoles()
        {
            IList<string> roles = new List<string> { "Роль не определена"};
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
        }
    }
}