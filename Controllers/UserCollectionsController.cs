using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using kursach.Models;

namespace kursach.Controllers
{
    public class UserCollectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserCollections
        public ActionResult Index()
        {
            var userCollections = db.UserCollection.Include(u => u.Collection);
            return View(userCollections.ToList());
        }

        // GET: UserCollections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCollection userCollection = db.UserCollection.Find(id);
            if (userCollection == null)
            {
                return HttpNotFound();
            }
            return View(userCollection);
        }

        // GET: UserCollections/Create
        public ActionResult Create()
        {
            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name");
            return View();
        }

        // POST: UserCollections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CollectionId,NameId")] UserCollection userCollection)
        {
            if (ModelState.IsValid)
            {
                db.UserCollection.Add(userCollection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CollectionId = new SelectList(db.Collections, "Id", "Name", userCollection.UserId);
            return View(userCollection);
        }

        // GET: UserCollections/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ApplicationUser user = await db.Users
                .Include(w => w.UserCollections.Select(z => z.Collection))
                .FirstOrDefaultAsync(u => u.Id == id)
                .ConfigureAwait(false);
            ViewBag.Collections = user.UserCollections.Select(x => x.Collection).ToList();
            return View(user);
        }

        // POST: UserCollections/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser user, int[] selectedCollections)
        {
            // TO DO: Раскоментировать и доделать!!!
            //if (ModelState.IsValid)
            //{
            //    foreach (var item in db.Collections.Where(item => selectedCollections.Contains(item.Id)))
            //    {
            //        user.UserCollections.Add(item);
            //    }
            //}
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: UserCollections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCollection userCollection = db.UserCollection.Find(id);
            if (userCollection == null)
            {
                return HttpNotFound();
            }
            return View(userCollection);
        }

        // POST: UserCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserCollection userCollection = db.UserCollection.Find(id);
            db.UserCollection.Remove(userCollection);
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
