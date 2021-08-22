using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public ActionResult Details(int? id)
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
            return View(collectionItem);
        }

        // GET: CollectionItems/Create
        public ActionResult Create()
        {
            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name");
            return View();
        }

        // POST: CollectionItems/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CollectionId,String1Value,String2Value,String3Value,Number1Value,Number2Value,Number3Value,Date1Value,Date2Value,Date3Value,Markdown1Value,Markdown2Value,Markdown3Value,Checkbox1Value,Checkbox2Value,Checkbox3Value")] CollectionItem collectionItem)
        {
            if (ModelState.IsValid)
            {
                db.CollectionItems.Add(collectionItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name", collectionItem.CollectionId);
            return View(collectionItem);
        }

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
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CollectionId,String1Value,String2Value,String3Value,Number1Value,Number2Value,Number3Value,Date1Value,Date2Value,Date3Value,Markdown1Value,Markdown2Value,Markdown3Value,Checkbox1Value,Checkbox2Value,Checkbox3Value")] CollectionItem collectionItem)
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

        // GET: CollectionItems/Delete/5
        public ActionResult Delete(int? id)
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
            return View(collectionItem);
        }

        // POST: CollectionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CollectionItem collectionItem = db.CollectionItems.Find(id);
            db.CollectionItems.Remove(collectionItem);
            db.SaveChanges();
            return RedirectToAction("Index");
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
