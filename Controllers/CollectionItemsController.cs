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

        //[HttpGet]
        //public ActionResult Create(int id)
        //{
        //    Collection collection = db.Collections.Find(id);
        //    if (collection == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    CollectionItem collectionItem = new CollectionItem
        //    {
        //        CollectionId = id,
        //        CollectionOfItem = collection
        //    };
        //    return View(collectionItem);
        //}

        //// POST: CollectionItems/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(CollectionItem collectionItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.CollectionItems.Add(collectionItem);
        //        db.SaveChanges();
        //        return RedirectToAction("Details", "CollectionItems");
        //    }
        //    return View(collectionItem);
        //}

        [HttpGet]
        public ActionResult Create2(int id)
        {
            Collection collection = db.Collections.Find(id);
            if (collection == null)
            {
                return HttpNotFound();
            }
            //CollectionItem collectionItem = new CollectionItem
            //{
            //    CollectionId = id,
            //    CollectionOfItem = collection
            //};
            ViewBag.CollectionId = id;
            return View(new CollectionItem());
        }

        // POST: CollectionItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2(CollectionItem collectionItem)
        {
            db.CollectionItems.Add(collectionItem);
            db.SaveChanges();
            return RedirectToAction("Details", "CollectionItems", new { id = collectionItem.CollectionId});
            //if (ModelState.IsValid)
            //{
            //    db.CollectionItems.Add(collectionItem);
            //    db.SaveChanges();
            //    return RedirectToAction("Details", "CollectionItems");
            //}
            //return View(collectionItem);
        }



        //public async Task<ActionResult> GetMyCollections()
        //{

        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        ApplicationUser user = await db.Users
        //            .Include(w => w.UserCollections.Select(z => z.Collection.CollectionTopic))
        //            .FirstOrDefaultAsync(u => u.Id == userId)
        //            .ConfigureAwait(false);

        //        var collections = user.UserCollections.Select(x => x.Collection).ToList();
        //        return View(collections);
        //    }
        //}

        // GET: CollectionItems/Create


        // GET: CollectionItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionItem collectionItem = db.CollectionItems.Find(id);
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
        public ActionResult Edit(CollectionItem collectionItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collectionItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name", collectionItem.CollectionId);
            return View(collectionItem);
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
