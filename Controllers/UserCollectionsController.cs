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
using Microsoft.AspNet.Identity;

namespace kursach.Controllers
{
    public class UserCollectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UserCollections
        public ActionResult Index()
        {
            var userCollections = db.UserCollections.Include(u => u.Collection);
            return View(userCollections.ToList());
        }

        // GET: UserCollections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCollection userCollection = db.UserCollections.Find(id);
            if (userCollection == null)
            {
                return HttpNotFound();
            }
            return View(userCollection);
        }

        [HttpPost]
        public async Task<ActionResult> AddInMyCollections(int id)
        {
            UserCollection userCollection = db.UserCollections.Find(HttpContext.User.Identity.GetUserId(), id);
            if (ModelState.IsValid && userCollection == null)
            {
                db.UserCollections.Add(new UserCollection
                {
                    UserId = HttpContext.User.Identity.GetUserId(),
                    CollectionId = id
                });
                await db.SaveChangesAsync();
            }
            return RedirectToAction("GetMyCollections", "Home"); 
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
            ViewBag.Collections = user.UserCollections.Select(x => x.User).ToList();
            //Collection collection = ;
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
            return RedirectToAction("GetUsers", "Home");
        }

        // GET: UserCollections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCollection userCollection = db.UserCollections.Find(id);
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
            UserCollection userCollection = db.UserCollections.Find(id);
            db.UserCollections.Remove(userCollection);
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
