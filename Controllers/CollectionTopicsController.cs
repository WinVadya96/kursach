using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kursach.Models;

namespace kursach.Controllers
{
    public class CollectionTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CollectionTopics
        public ActionResult Index()
        {
            return View();
        }

        // GET: CollectionTopics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionTopic collectionTopic = db.CollectonTopic.Find(id);
            if (collectionTopic == null)
            {
                return HttpNotFound();
            }
            return View(collectionTopic);
        }

        // GET: CollectionTopics/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Collections, "Id", "Name");
            return View();
        }

        // POST: CollectionTopics/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] CollectionTopic collectionTopic)
        {
            if (ModelState.IsValid)
            {
                db.CollectonTopic.Add(collectionTopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Collections, "Id", "Name", collectionTopic.Id);
            return View(collectionTopic);
        }

        // GET: CollectionTopics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionTopic collectionTopic = db.CollectonTopic.Find(id);
            if (collectionTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Collections, "Id", "Name", collectionTopic.Id);
            return View(collectionTopic);
        }

        // POST: CollectionTopics/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TopicId")] CollectionTopic collectionTopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(collectionTopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Collections, "Id", "Name", collectionTopic.Id);
            return View(collectionTopic);
        }

        // GET: CollectionTopics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionTopic collectionTopic = db.CollectonTopic.Find(id);
            if (collectionTopic == null)
            {
                return HttpNotFound();
            }
            return View(collectionTopic);
        }

        // POST: CollectionTopics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CollectionTopic collectionTopic = db.CollectonTopic.Find(id);
            db.CollectonTopic.Remove(collectionTopic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditTable()
        {
            return View(new Collection());
        }

        [HttpPost]
        public ActionResult EditTablePost(Collection collection)
        {
            return View();
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
