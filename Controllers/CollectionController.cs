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

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            return View(collection);
        }

        [HttpGet]
        public ActionResult EditScheme(int id)
        {
            SelectList topics = new SelectList(db.CollectionTopics, "Id", "Name");
            ViewBag.Topics = topics;
            Collection collection = db.Collections.Find(id);
            return View(collection);
        }

        [HttpPost]
        public ActionResult EditScheme(Collection collection)
        {
            db.Entry(collection).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GetCollections", "Home");
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

        // GET: Collection/Edit/5 
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            //IList<CollectionItem> collectionItem = new List<CollectionItem>();
            //ViewBag.Topics = new SelectList(db.CollectionTopic, "Id", "Name");
            CollectionItem collectionItem = new CollectionItem
            {
                CollectionOfItem = collection
            };
            //ViewBag.Collection = new SelectList(db.Collections, "Id", "Name");
            return View(collectionItem);
            //return View(collectionItem);
        }

        [HttpPost]
        public ActionResult Edit(CollectionItem collectionItem)
        {
            db.Entry(collectionItem).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GetCollections", "Home");
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