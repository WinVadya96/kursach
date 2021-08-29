using kursach.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace kursach.Controllers
{
    public class CollectionController : Controller
    {
        public CollectionController()
        {
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult EditScheme(int id)
        {
            SelectList topics = new SelectList(db.CollectionTopics, "Id", "Name");
            ViewBag.Topics = topics;
            Collection collection = db.Collections.Find(id);
            return View(collection);
        }

        [HttpPost]
        public async Task<ActionResult> EditScheme(Collection collection)
        {
            db.Entry(collection).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("GetMyCollections", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var dbSet = context.Set<Collection>();
                var collection = await dbSet.FindAsync(id).ConfigureAwait(false);

                if (collection != null)
                {
                    dbSet.Remove(collection);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }

                return RedirectToAction("GetCollections", "Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteMyCollection(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var dbSet = context.Set<Collection>();
                var collection = await dbSet.FindAsync(id).ConfigureAwait(false);
                var userId = HttpContext.User.Identity.GetUserId();

                if (collection != null)
                {
                    var user = context.UserCollections.First(p => p.UserId == userId);
                    var myCollection = context.UserCollections.First(p => p.CollectionId == id);
                    context.UserCollections.Remove(myCollection);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }

                return RedirectToAction("GetMyCollections", "Home");
            }
        }

        // GET: Collection/Create
        [HttpGet]
        public ActionResult Create()
        {
            SelectList topics = new SelectList(db.CollectionTopics, "Id", "Name");
            ViewBag.Topics = topics;
            return View(new Collection());
        }

        // POST: Collection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Collections.Add(collection);
                await db.SaveChangesAsync();
                return RedirectToAction("GetCollections", "Home");
            };          

            SelectList topics = new SelectList(db.CollectionTopics, "Id", "Name");
            ViewBag.Topics = topics;
            return View(collection);
        }

        // GET: Collection/CreateMyCollection
        [HttpGet]
        public ActionResult CreateMyCollection()
        {
            SelectList topics = new SelectList(db.CollectionTopics, "Id", "Name");
            ViewBag.Topics = topics;
            return View(new Collection());
        }

        // POST: Collection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMyCollection(Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Collections.Add(collection);
                await db.SaveChangesAsync();
                db.UserCollections.Add(new UserCollection
                {
                    UserId = HttpContext.User.Identity.GetUserId(),
                    CollectionId = collection.Id
                });
                await db.SaveChangesAsync();
                return RedirectToAction("GetMyCollections", "Home");
            };

            SelectList topics = new SelectList(db.CollectionTopics, "Id", "Name");
            ViewBag.Topics = topics;
            return View(collection);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}