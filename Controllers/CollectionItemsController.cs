using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using kursach.Models;

namespace kursach.Controllers
{
    public class CollectionItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CollectionItems
        public ActionResult Index()
        {
            var collectionItems = db.CollectionItems.Include(c => c.CollectionOfItem);
            return View(collectionItems.ToList());
        }

        // GET: CollectionItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var collection = await db.Collections
                    .Include(i => i.Items)
                    .FirstOrDefaultAsync(c => c.Id == id)
                    .ConfigureAwait(false); 
                return View(collection);
            }
        }

        [HttpGet]
        public ActionResult Create(int id)
        {
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            CollectionItem collectionItem = new CollectionItem
            {
                CollectionId = id,
                CollectionOfItem = collection
            };
            return View(collectionItem);
        }

        // POST: CollectionItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CollectionItem collectionItem)
        {
            if (ModelState.IsValid)
            {
                db.CollectionItems.Add(collectionItem);
                await db.SaveChangesAsync().ConfigureAwait(false);
                return RedirectToAction("Details", "CollectionItems", new { id = collectionItem.CollectionId });
            }
            else // return not created item back to creation menu
            {
                collectionItem.CollectionOfItem = await db.Collections
                                                          .FindAsync(collectionItem.CollectionId)
                                                          .ConfigureAwait(false);

                return View(collectionItem);
            }
        }

        // GET: CollectionItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var collectionItem = await db.CollectionItems
                                         .Include(i => i.CollectionOfItem)
                                         .FirstOrDefaultAsync(i => i.Id == id)
                                         .ConfigureAwait(false);

            if (collectionItem == null)
            {
                return HttpNotFound();
            }

            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name", collectionItem.CollectionId);
            return View(collectionItem);
        }

        // POST: CollectionItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CollectionItem collectionItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collectionItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Details", "CollectionItems", new { id = collectionItem.CollectionId });
            }
            else // return not created item back to creation menu
            {
                collectionItem.CollectionOfItem = await db.Collections
                                                          .FindAsync(collectionItem.CollectionId)
                                                          .ConfigureAwait(false);

                ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name", collectionItem.CollectionId);
                return View(collectionItem);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var dbSet = context.Set<CollectionItem>();
                var collectionItem = await dbSet.FindAsync(id).ConfigureAwait(false);

                if (collectionItem != null)
                {
                    dbSet.Remove(collectionItem);
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }

                return RedirectToAction("Details", "CollectionItems", new { id = collectionItem.CollectionId});
            }
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
