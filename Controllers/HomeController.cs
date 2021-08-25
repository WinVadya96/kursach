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
using kursach.Controllers;
using System.Net;

namespace kursach.Controllers
{
    [RequireHttps]

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> GetMyCollections()
        {

            var userId = HttpContext.User.Identity.GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            using (ApplicationDbContext db = new ApplicationDbContext())
            {                
                ApplicationUser user = await db.Users
                    .Include(w => w.UserCollections.Select(z => z.Collection.CollectionTopic))
                    .FirstOrDefaultAsync(u => u.Id == userId)
                    .ConfigureAwait(false);

                var collections = user.UserCollections.Select(x => x.Collection).ToList();
                return View(collections);
            }            
        }

        public async Task<ActionResult> GetCollections()
        {
            IList<Collection> collections = new List<Collection>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                collections = await db.Collections.Include(m => m.CollectionTopic).ToListAsync().ConfigureAwait(false);
                //var collectionItem = await db.CollectionItems.ToListAsync().ConfigureAwait(false);
                var collectionItem = db.CollectionItems;

                //ViewData["CollectionItems"] = db.CollectionItems;
                return View(collections);
            }    
        }

        public async Task<ActionResult> GetCollectionsItem(int id)
        {
            IList<CollectionItem> collectionItems = new List<CollectionItem>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                collectionItems = await db.CollectionItems.Include(m => m.CollectionOfItem).ToListAsync().ConfigureAwait(false);
            }
            return View(collectionItems);
        }

        [Authorize(Roles = "admin")]
        public async Task<ActionResult> GetUsers()
        {
            IList<ApplicationUser> users = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                users = await db.Users.ToListAsync().ConfigureAwait(false);
            }
            return View(users);
        }

        [Authorize]
        public ActionResult GetMyRoles()
        {
            IList<string> roles = new List<string> { "Role not defined" };
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser user = userManager.FindByEmail(User.Identity.Name);
            if (user != null)
                roles = userManager.GetRoles(user.Id);
            return View(roles);
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

        public ActionResult Admin(string id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ApplicationUser user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var z = user.IsAdminIn.ToString();
                user.IsAdminIn = z == "False";
                db.SaveChanges();
                return RedirectToAction("GetUsers");
            }
        }

                
    }
}