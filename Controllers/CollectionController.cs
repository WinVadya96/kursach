using kursach.Models;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult EditScheme()
        {
            SelectList topics = new SelectList(db.CollectionTopic, "Id", "Name");
            //ViewBag.Topics = topics;
            return View(topics);
        }

        [HttpPost]
        public ActionResult EditScheme(Collection collection)
        {
            db.Collections.Add(collection);
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

        // GET: Collection/Create
        [HttpGet]
        public ActionResult Create()
        {
            SelectList topics = new SelectList(db.CollectionTopic, "Id", "Name");
            ViewBag.Topics = topics;
            return View(new Collection());
        }

        // POST: Collection/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Collection collection)
        {
            if (ModelState.IsValid)
            {
                db.Collections.Add(collection);
                db.SaveChanges();
                return RedirectToAction("GetCollections", "Home");
            }

            SelectList topics = new SelectList(db.CollectionTopic, "Id", "Name");
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
            ViewBag.Topics = new SelectList(db.CollectionTopic, "Id", "Name");
            return View(collection);
        }

        [HttpPost]
        public ActionResult Edit(Collection collection)
        {
            //db.Collections.Add(collection);
            db.Entry(collection).State = EntityState.Modified;
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