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
    public class CollectionTopicsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> GetCollectionsDouble()
        {
            IList<Collection> collections = new List<Collection>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                collections = await db.Collections.Include(m => m.CollectionTopic).ToListAsync();
            }
            return View(collections);
        }
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
            CollectionTopic collectionTopic = db.CollectionTopic.Find(id);
            if (collectionTopic == null)
            {
                return HttpNotFound();
            }
            return View(collectionTopic);
        }

        // GET: CollectionTopics/Create
        public ActionResult Create()
        {
            SelectList topics = new SelectList(db.CollectionTopic, "Id", "Name");
            ViewBag.Topics = topics;
            return View(new Collection());
        }

        // POST: CollectionTopics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Collection collection)
        {
            //[Bind(Include = "Id,Name,Discription,CollectonTopicId")]
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


        // GET: CollectionTopics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CollectionTopic collectionTopic = db.CollectionTopic.Find(id);
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

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
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
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            Collection collection = db.Collections.Find(id);
            db.Collections.Remove(collection);
            db.SaveChanges();
            return RedirectToAction("GetCollection", "Home");
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    ApplicationUser us = db.Users.Find(id);
            //    if (us == null)
            //    {
            //        return HttpNotFound();
            //    }
            //    db.Users.Remove(us);
            //    db.SaveChanges();
            //    return RedirectToAction("GetUsers");
            //}
        }




        [HttpGet]
        public ActionResult EditTable()
        {
            //ViewBag.Id = new SelectList(db.CollectionTopic, "Id", "Name");
            SelectList topics = new SelectList(db.CollectionTopic, "Id", "Name");
            ViewBag.Topics = topics;
            return View(new Collection());
        }

        [HttpPost]
        public ActionResult EditTablePost(Collection collection)
        {
            db.Collections.Add(collection);
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
