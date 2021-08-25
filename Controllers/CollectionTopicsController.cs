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
            CollectionTopic collectionTopic = db.CollectionTopics.Find(id);
            if (collectionTopic == null)
            {
                return HttpNotFound();
            }
            return View(collectionTopic);
        }

        // GET: CollectionTopics/Create
        public ActionResult Create()
        {
            SelectList topics = new SelectList(db.CollectionTopics, "Id", "Name");
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

            SelectList topics = new SelectList(db.CollectionTopics, "Id", "Name");
            ViewBag.Topics = topics;
            return View(collection);
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
            SelectList topics = new SelectList(db.CollectionTopics, "Id", "Name");
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
